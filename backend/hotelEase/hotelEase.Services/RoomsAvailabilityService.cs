using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class RoomsAvailabilityService
        : BaseCRUDService<Model.RoomAvailability, RoomsAvailabilitySearchObject, Database.RoomAvailability, RoomsAvailabilityUpsertRequest, RoomsAvailabilityUpsertRequest>,
          IRoomsAvailabilityService
    {
        private readonly HotelEaseContext _context;

        public RoomsAvailabilityService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public List<Model.RoomAvailability> GetAvailabilityRooms(int roomId, int month, int year)
        {
            // 1. Dohvati sve rezervacije za tu sobu u odabranom mjesecu/godini
            var reservations = _context.Reservations
                .Where(r => r.RoomId == roomId &&
                           ((r.CheckInDate.Month == month && r.CheckInDate.Year == year) ||
                            (r.CheckOutDate.Month == month && r.CheckOutDate.Year == year) ||
                            (r.CheckInDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) &&
                             r.CheckOutDate > new DateTime(year, month, 1))))
                .ToList();

            var daysInMonth = DateTime.DaysInMonth(year, month);
            var availabilityList = new List<Model.RoomAvailability>();

            for (int day = 1; day <= daysInMonth; day++)
            {
                var currentDate = new DateTime(year, month, day);

                // Provjera da li datum pada unutar bilo koje rezervacije
                bool isBooked = reservations.Any(r =>
                    currentDate >= r.CheckInDate.Date &&
                    currentDate < r.CheckOutDate.Date &&
                    (r.Status == null || r.Status.ToLower() != "cancelled"));

                int statusCode;
                if (isBooked)
                {
                    statusCode = 1; // booked
                }
                else
                {
                    // limited ako ima manje od 3 dana slobodno prije nego počne rezervacija
                    bool nearBooking = reservations.Any(r =>
                        r.CheckInDate.Date > currentDate &&
                        (r.CheckInDate.Date - currentDate).TotalDays <= 3);

                    statusCode = nearBooking ? 2 : 0; // 2 = limited, 0 = available
                }

                availabilityList.Add(new Model.RoomAvailability
                {
                    RoomId = roomId,
                    Date = currentDate,
                    Status = statusCode
                });
            }

            return availabilityList;
        }
    }
}