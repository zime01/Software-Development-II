using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface ICitiesService : ICRUDService<Model.City, CititesSearchObject, CitiesUpsertRequest, CitiesUpsertRequest>
    {
    }
}
