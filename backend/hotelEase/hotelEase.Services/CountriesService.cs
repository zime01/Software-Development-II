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
    public class CountriesService : BaseCRUDService<Model.Country, CountriesSearchObject, Database.Country, CountriesUpsertRequest, CountriesUpsertRequest>, ICountriesService
    {
        public CountriesService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
