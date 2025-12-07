using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using hotelEase.Services.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment = hotelEase.Model.Payment;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : BaseCRUDController<Model.Payment, PaymentsSearchObject, PaymentsUpsertRequest, PaymentsUpsertRequest>
    {
        private readonly IPaymentsService _payments;
        private readonly HotelEaseContext _context;
        private readonly INotificationsService _notificationsService;


        public PaymentsController(IPaymentsService service, HotelEaseContext context, INotificationsService notificationsService) : base(service)
        {
            _payments = service;
            _context = context;
            _notificationsService = notificationsService;
        }

        
        [HttpPost("create-intent")]
        public async Task<ActionResult<Model.Payment>> CreateIntent([FromBody] PaymentsCreateIntentRequest request)
        {
            var result = await _payments.CreateIntentAsync(request);
            return Ok(result);
        }

        [HttpPost("update-status")]
        public async Task<ActionResult<Model.Payment>> UpdateStatus([FromBody] PaymentsStatusUpdateRequest request)
        {
            var result = await _payments.UpdateStatusAsync(request);
            return Ok(result);
        }

        [HttpGet("by-reservation/{reservationId}")]
        public ActionResult<List<Model.Payment>> GetByReservation(int reservationId)
        {
            var list = _payments.GetByReservation(reservationId);
            return Ok(list);
        }

        
        [HttpPost("stripe-create-intent")]
        public async Task<IActionResult> StripeCreateIntent([FromBody] CreatePaymentIntentRequest request)
        {
            var p = await _payments.CreateStripePaymentIntentAsync(
                request.ReservationId,
                request.Amount,
                request.Currency
            );

            return Ok(new
            {
                paymentId = p.Id,                   
                paymentIntentId = p.ProviderPaymentId, 
                clientSecret = p.ClientSecret         
            });
        }


        [HttpPost("stripe-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] PaymentsCreateIntentRequest request)
        {

            var reservation = await _context.Reservations.FindAsync(request.ReservationId);
            if (_context.Payments.Any(p => p.ReservationId == reservation.Id && p.Status == "Succeeded"))
            {
                return BadRequest("Reservation already paid.");
            }
            if (reservation == null) return NotFound("Reservation not found");
            if (reservation.TotalPrice == null) return BadRequest("Reservation total price is null");

            // 1️⃣ Kreiraj Payment zapis u bazi
            var payment = new Services.Database.Payment
            {
                ReservationId = reservation.Id,
                Amount = reservation.TotalPrice,
                Currency = request.Currency,
                Status = "Pending",
                Provider = "stripe",
                CreatedAt = DateTime.UtcNow
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // 2️⃣ Kreiraj Stripe Checkout session
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",

                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
    {
        new Stripe.Checkout.SessionLineItemOptions
        {
            PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
            {
                Currency = request.Currency,
                ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                {
                    Name = $"Reservation #{reservation.Id}"
                },
                UnitAmount = (long)(reservation.TotalPrice * 100),
            },
            Quantity = 1
        }
    },

                SuccessUrl = "https://yourapp.com/payment-success?paymentId=" + payment.Id,
                CancelUrl = "https://yourapp.com/payment-cancel?paymentId=" + payment.Id,

                Metadata = new Dictionary<string, string>
    {
        { "paymentId", payment.Id.ToString() }
    },

                PaymentIntentData = new Stripe.Checkout.SessionPaymentIntentDataOptions
                {
                    Metadata = new Dictionary<string, string>
        {
            { "paymentId", payment.Id.ToString() }
        }
                }
            };

            var service = new Stripe.Checkout.SessionService();
            var session = await service.CreateAsync(options);

            return Ok(new
            {
                url = session.Url,
                sessionId = session.Id,
                paymentId = payment.Id,
            });
        }

        [HttpGet("status/{paymentId}")]
        public async Task<IActionResult> GetStatus(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null) return NotFound();

            return Ok(new { status = payment.Status });
        }
        [AllowAnonymous]
        [HttpPost("stripe-webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = Stripe.EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                "whsec_c1d694b83cbf39d149c7a811875fecb7b3bd4dbcddad054296091437e310574e"
            );

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                if (session != null)
                {
                    var paymentId = int.Parse(session.Metadata["paymentId"]);
                    var payment = await _context.Payments
                        .Include(p => p.Reservation)
                        .ThenInclude(r => r.User)
                        .FirstOrDefaultAsync(p => p.Id == paymentId);

                    if (payment != null)
                    {
                        payment.Status = "Succeeded";
                        payment.UpdatedAt = DateTime.UtcNow;
                        payment.ProviderPaymentId = session.PaymentIntentId;

                        // Update rezervacije
                        payment.Reservation.Status = "Confirmed";

                        // Pošalji email
                        var message = new NotificationMessage
                        {
                            Type = "email",
                            To = payment.Reservation.User.Email,
                            Subject = "Payment succeeded",
                            Body = $"✅ Plaćanje za rezervaciju #{payment.Reservation.Id} u iznosu {payment.Amount} {payment.Currency} je uspješno."
                        };
                        await _notificationsService.SendAndStoreNotificationAsync(message, payment.Reservation.UserId);

                        await _context.SaveChangesAsync();
                    }
                }
            }

            return Ok();
        }
    }
}
