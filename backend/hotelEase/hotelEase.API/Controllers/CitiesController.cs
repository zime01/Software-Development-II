using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : BaseCRUDController<Model.City, CititesSearchObject, CitiesUpsertRequest, CitiesUpsertRequest>
    {
        public CitiesController(ICitiesService service) : base(service) { }
    }
}
