using hotelEase.Model;
using hotelEase.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IRoomTypesService : IService<Model.RoomType, RoomTypesSearchObject>
    {
        //List<RoomType> GetList(RoomTypesSearchObject searchObject);
    }
}
