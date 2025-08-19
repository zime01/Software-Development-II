using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class PaymentsService
        : BaseCRUDService<Model.Payment, PaymentsSearchObject, Database.Payment, PaymentsUpsertRequest, PaymentsUpsertRequest>,
          IPaymentsService
    {
        private readonly INotificationsService _notificationsService;
        public PaymentsService(HotelEaseContext context, IMapper mapper, INotificationsService notificationsService)
            : base(context, mapper)
        {
            _notificationsService = notificationsService;
        }

        
        public override IQueryable<Database.Payment> AddFilter(PaymentsSearchObject search, IQueryable<Database.Payment> query)
        {
            query = base.AddFilter(search, query);
            if (search == null) return query;

            if (search.ReservationId.HasValue)
                query = query.Where(x => x.ReservationId == search.ReservationId.Value);

            if (!string.IsNullOrWhiteSpace(search.Status))
                query = query.Where(x => x.Status == search.Status);

            if (!string.IsNullOrWhiteSpace(search.Provider))
                query = query.Where(x => x.Provider == search.Provider);

            return query;
        }

        public async Task<Model.Payment> CreateIntentAsync(PaymentsCreateIntentRequest request)
        {
            var reservation = await Context.Reservations
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == request.ReservationId)
                ?? throw new Exception("Reservation not found");

            var amount = request.OverrideAmount ?? reservation.TotalPrice;

            var db = new Database.Payment
            {
                ReservationId = reservation.Id,
                Provider = request.Provider,
                Amount = amount,
                Currency = request.Currency,
                Status = "processing",
                ProviderPaymentId = string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            Context.Payments.Add(db);
            await Context.SaveChangesAsync();

            return Mapper.Map<Model.Payment>(db);
        }

        public async Task<Model.Payment> UpdateStatusAsync(PaymentsStatusUpdateRequest request)
        {
            var db = await Context.Payments.FindAsync(request.PaymentId)
                     ?? throw new Exception("Payment not found");

            db.Status = request.NewStatus;
            if (!string.IsNullOrWhiteSpace(request.ProviderPaymentId))
                db.ProviderPaymentId = request.ProviderPaymentId;

            if (request.NewStatus == "succeeded")
            {
                var res = await Context.Reservations.FindAsync(db.ReservationId);
                if (res != null)
                {
                    res.Status = "Confirmed";
                }
            }

            db.UpdatedAt = DateTime.UtcNow;
            await Context.SaveChangesAsync();

            return Mapper.Map<Model.Payment>(db);
        }

        public List<Model.Payment> GetByReservation(int reservationId)
        {
            var list = Context.Payments.AsNoTracking()
                         .Where(p => p.ReservationId == reservationId)
                         .OrderByDescending(p => p.CreatedAt)
                         .ToList();

            return Mapper.Map<List<Model.Payment>>(list);
        }

        public async Task<Model.Payment> CreateStripePaymentIntentAsync(int reservationId, decimal amount, string currency = "usd")
        {
            var reservation = await Context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservation == null)
                throw new Exception("Reservation not found");

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)Math.Round(amount * 100m), // centi
                Currency = currency.ToLower(),
                Metadata = new Dictionary<string, string>
                {
                    { "reservationId", reservationId.ToString() }
                },
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            var payment = new Database.Payment
            {
                ReservationId = reservationId,
                Provider = "stripe",
                ProviderPaymentId = paymentIntent.Id,   // npr. "pi_..."
                Amount = amount,
                Currency = currency.ToUpper(),
                Status = paymentIntent.Status,          // "requires_payment_method"
                CreatedAt = DateTime.UtcNow
            };

            Context.Payments.Add(payment);
            await Context.SaveChangesAsync();

            // vraćamo Model.Payment + clientSecret (ne upisujemo clientSecret u DB)
            return new Model.Payment
            {
                Id = payment.Id,
                ReservationId = reservationId,
                Provider = payment.Provider,
                ProviderPaymentId = paymentIntent.Id,
                Amount = amount,
                Currency = payment.Currency,
                Status = payment.Status,
                ClientSecret = paymentIntent.ClientSecret
            };
        }

        public async Task UpdateStripePaymentStatusAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            var pi = await service.GetAsync(paymentIntentId);

            var payment = await Context.Payments
                .Include(p => p.Reservation)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(p => p.ProviderPaymentId == paymentIntentId);

            if (payment == null) return;

            payment.Status = pi.Status;
            payment.UpdatedAt = DateTime.UtcNow;

            if (pi.Status == "succeeded")
            {
                // update rezervacije
                payment.Reservation.Status = "Confirmed";

                var message = new NotificationMessage
                {
                    Type = "email",
                    To = payment.Reservation.User.Email,
                    Subject = "Payment succeeded",
                    Body = $"✅ Vaše plaćanje za rezervaciju #{payment.Reservation.Id} u iznosu {payment.Amount} {payment.Currency} je uspješno. " +
                           $"Status rezervacije: Confirmed."
                };

                await _notificationsService.SendAndStoreNotificationAsync(message, payment.Reservation.UserId);
            }
            else if (pi.Status == "failed" || pi.Status == "canceled")
            {
                var msg = new NotificationMessage
                {
                    Type = "email",
                    To = payment.Reservation.User.Email,
                    Subject = "Payment failed",
                    Body = $"❌ Plaćanje za rezervaciju #{payment.Reservation.Id} nije uspjelo. Pokušajte ponovo ili kontaktirajte podršku."
                };

                await _notificationsService.SendAndStoreNotificationAsync(msg, payment.Reservation.UserId);
            }

            await Context.SaveChangesAsync();
        }

    }
}
