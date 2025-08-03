using hotelEase.Model;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : BaseController<Model.Hotel, HotelsSearchObject>
    {
        public HotelsController(IHotelsService service) : base(service) { }
    }
}
