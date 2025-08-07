using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IRoomsAvailabilityService : ICRUDService<Model.RoomAvailability, RoomsAvailabilitySearchObject, RoomsAvailabilityUpsertRequest, RoomsAvailabilityUpsertRequest>
    {
        List<Model.RoomAvailability> GetAvailabilityRooms(int roomId, int month, int year);
    }
}
