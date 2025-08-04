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
    public interface IHotelsService : ICRUDService<Hotel, HotelsSearchObject, HotelsInsertRequest, HotelsUpdateRequest>
    {
        public Hotel Activate(int id);
        public Hotel Edit(int id);
        public Hotel Hide(int id);
        public List<string> AllowedActions(int id);

    }
}
