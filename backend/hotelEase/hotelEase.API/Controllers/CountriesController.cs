using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : BaseCRUDController<Model.Country, CountriesSearchObject, CountriesUpsertRequest, CountriesUpsertRequest>
    {
        public CountriesController(ICountriesService service) : base(service) { }
    }
}
