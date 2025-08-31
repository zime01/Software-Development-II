using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using hotelEase.Services.Database;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    public class RoomsController : BaseCRUDController<Model.Room, RoomsSearchObject, RoomsInsertRequest, RoomsUpdateRequest>
    {

        
        public RoomsController(IRoomsService service) : base(service) { }

        [HttpGet("by-hotel/{hotelId}")]
        public IActionResult GetByHotelId(int hotelId)
        {
            var result = (_service as IRoomsService).GetRoomByHotel(hotelId);

            return Ok(result);
        }

        [HttpGet("details/{room_id}")]
        public ActionResult<RoomDTO> GetRoom(int room_id)
        {
            var room = (_service as IRoomsService).GetRoomDtoById(room_id);

            if (room == null)
                return NotFound();

            return Ok(room);
        }
    }
}
