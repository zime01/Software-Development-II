using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class RoomTypesController : BaseCRUDController<RoomType, RoomTypesSearchObject, RoomTypesUpsertRequest, RoomTypesUpsertRequest>
    {
        

        public RoomTypesController(IRoomTypesService service) : base(service) { }

        [Authorize(Roles = "Admin")]
        public override ActionResult<RoomType> Insert(RoomTypesUpsertRequest request)
        {
            return base.Insert(request);
        }
        [Authorize(Roles = "Admin")]
        public override ActionResult<RoomType> Update(int id, RoomTypesUpsertRequest request)
        {
            return base.Update(id, request);
        }

        [Authorize(Roles = "Admin")]
        public override ActionResult<RoomType> Delete(int id)
        {
            return base.Delete(id);
        }


        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public override PagedResult<RoomType> GetPaged([FromQuery] RoomTypesSearchObject searchObject)
        {
            return base.GetPaged(searchObject);
        }

        //[HttpGet]
        //public List<Model.RoomType> GetList([FromQuery] RoomTypesSearchObject searchObject)
        //{
        //    return _service.GetList(searchObject);
        //}
    }
}
