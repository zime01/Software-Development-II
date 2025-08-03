using hotelEase.Model;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypesController : BaseController<RoomType, RoomTypesSearchObject>
    {
        

        public RoomTypesController(IRoomTypesService service) : base(service) { }
        

        //[HttpGet]
        //public List<Model.RoomType> GetList([FromQuery] RoomTypesSearchObject searchObject)
        //{
        //    return _service.GetList(searchObject);
        //}
    }
}
