using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class RoomsAvailabilityService : BaseCRUDService<Model.RoomAvailability, RoomsAvailabilitySearchObject, Database.RoomAvailability, RoomsAvailabilityUpsertRequest, RoomsAvailabilityUpsertRequest>, IRoomsAvailabilityService
    {
        private readonly HotelEaseContext _context;
        public RoomsAvailabilityService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public List<Model.RoomAvailability> GetAvailabilityRooms(int roomId, int month, int year)
        {
            var availability = _context.RoomAvailabilities.Where(x => x.RoomId == roomId && x.Date.Month == month && x.Date.Year == year)
                .Select(x => new Model.RoomAvailability
                {
                    RoomId = x.RoomId,
                    Date = x.Date,
                    Status = x.Status,
                }).ToList();

            return availability;
        }
    }
}
