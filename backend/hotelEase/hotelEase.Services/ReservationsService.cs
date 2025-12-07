using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class ReservationsService : BaseCRUDService<Model.Reservation, ReservationsSearchObject, Database.Reservation, ReservationsUpsertRequest, ReservationsUpsertRequest>, IReservationsService
    {
        private readonly INotificationsService _notificationsService;
        public ReservationsService(HotelEaseContext context, IMapper mapper, INotificationsService notificationsService) : base(context, mapper)
        {
            _notificationsService = notificationsService;
        }

        public override IQueryable<Database.Reservation> AddInclude(ReservationsSearchObject search, IQueryable<Database.Reservation> query)
        {
            query = query.Include(r => r.Room)
                 .ThenInclude(r => r.Hotel)
                 .Include(r => r.Room)
                 .ThenInclude(r => r.Assets)
                 .Include(h=>h.Payments);

            

            return query;
        }

        public List<Model.Reservation> GetByUserId(int id)
        {
            var query = Context.Reservations
                .Include(r => r.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(r => r.Room)
                    .ThenInclude(r => r.Assets)  // 🔹 dodano da povuče slike
                .Include(r => r.User)
                .Where(r => r.UserId == id && (r.IsDeleted == null || r.IsDeleted == false))
                .AsQueryable();

            var list = query.ToList();

            return Mapper.Map<List<Model.Reservation>>(list);
        }

        public async Task<Model.Reservation> UpdateStatusAsync(int reservationId, string newStatus, int actingUserId)
        {
            var entity = Context.Reservations.Include(r => r.User)
                                             .Include(r => r.Room)
                                             .ThenInclude(r => r.Hotel)
                                             .FirstOrDefault(r => r.Id == reservationId);

            if (entity == null)
            {
                throw new Exception("Reservation not found");

            }
            entity.Status = newStatus;

            await Context.SaveChangesAsync();

            var message = new NotificationMessage
            {
                Type = "email",
                To = entity.User.Email,
                Subject = "Reservation status updated",
                Body = $"Your reservation #{entity.Id} status changed to {newStatus}"
            };

            await _notificationsService.SendAndStoreNotificationAsync(message, entity.UserId);

            return Mapper.Map<Model.Reservation>(entity);
        }

        public async Task<Model.Reservation> InsertAsync(ReservationsUpsertRequest request)
        {
            try
            {
                var entity = base.Insert(request);

                var message = new NotificationMessage
                {
                    Type = "email",
                    To = entity.User?.Email ?? "",
                    Subject = "Reservation Created",
                    Body = $"Your reservation #{entity.Id} for {entity.CheckInDate:dd.MM.yyyy} - {entity.CheckOutDate:dd.MM.yyyy} is created. Total: {entity.TotalPrice:C}."
                };

                // fire-and-forget da ne blokira Insert
                _ = _notificationsService.SendAndStoreNotificationAsync(message, entity.UserId);

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InsertAsync error: {ex}");
                throw;
            }
        }

        public List<Model.Reservation> GetReservationsByHotel(int hotelId)
        {
            var query = Context.Reservations
                .Include(r => r.Room)
                .ThenInclude(r => r.Hotel)
                .Where(r => r.Room.HotelId == hotelId && (r.IsDeleted == null || r.IsDeleted == false))
                .ToList();

            return Mapper.Map<List<Model.Reservation>>(query);
        }


        public List<Model.Reservation> GetReservationsByDate(ReservationsSearchObject search)
        {
            var query = Context.Reservations
                .Include(r => r.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(r => r.Room)
                    .ThenInclude(r => r.Assets)
                .Include(r => r.User)
                .Include(r => r.Payments)
                .Where(r => r.IsDeleted == null || r.IsDeleted == false)
                .AsQueryable();

            if (search?.Date != null)
            {
                var date = search.Date.Value.Date;

                query = query.Where(r =>
                    r.CheckInDate.Date <= date &&
                    r.CheckOutDate.Date >= date
                );
            }

            query = query.OrderBy(r => r.CheckInDate);

            var list = query.ToList();
            return Mapper.Map<List<Model.Reservation>>(list);
        }

    }
}
