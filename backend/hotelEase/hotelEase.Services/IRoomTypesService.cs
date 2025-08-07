using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IRoomTypesService : ICRUDService<Model.RoomType, RoomTypesSearchObject, RoomTypesUpsertRequest, RoomTypesUpsertRequest>
    {
        //List<RoomType> GetList(RoomTypesSearchObject searchObject);
    }
}
