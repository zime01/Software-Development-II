using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IRoomsService :ICRUDService<Model.Room, RoomsSearchObject, RoomsInsertRequest, RoomsUpdateRequest>
    {

        List<Model.Room> GetRoomByHotel(int hotelId);
    }

    
}
