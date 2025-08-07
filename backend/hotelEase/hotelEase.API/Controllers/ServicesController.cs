using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : BaseCRUDController<Model.Service, ServicesSearchObject, ServicesUpsertObject, ServicesUpsertObject>
    {
        public ServicesController(IServicesService service) : base(service) { }

        [HttpGet("by-hotel/{hotelId}")]
        public List<Model.Service> GetByHotelId(int hotelId)
        {
            return (_service as IServicesService).GetByHotelId(hotelId);
        }
    }
}
