using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    public class AssetsController : BaseCRUDController<Model.Asset, AssetsSearchObject, AssetsUpsertRequest, AssetsUpsertRequest>
    {
        public AssetsController(IAssetsService service) : base(service) { }

        [HttpGet("ByHotel/{hotelId}")]
        public IActionResult GetByHotelId(int hotelId)
        {
            var search = new AssetsSearchObject
            {
                HotelId = hotelId,
            };
            var result = (_service as IAssetsService).GetPaged(search);
            return Ok(result);
        }
    }
}
