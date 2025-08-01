using hotelEase.Model;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        protected IHotelsService _service;
        public HotelsController(IHotelsService service)
        {
            _service = service;
        }
        [HttpGet]
        public List<Hotel> GetList()
        {
            return _service.GetList();
        }
    }
}
