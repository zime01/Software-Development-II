using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsAvailabilityController : BaseCRUDController<Model.RoomAvailability, RoomsAvailabilitySearchObject, RoomsAvailabilityUpsertRequest, RoomsAvailabilityUpsertRequest>
    {
        private readonly IRoomsAvailabilityService _roomsAvailabilityService;
        public RoomsAvailabilityController(IRoomsAvailabilityService service) : base(service)
        {
            _roomsAvailabilityService = service;
        }

        [HttpGet("color-coded")]
        public List<Model.RoomAvailability> GetAvailabilityRooms([FromQuery] int roomId,[FromQuery] int month,[FromQuery] int year)
        {
            return _roomsAvailabilityService.GetAvailabilityRooms(roomId, month, year);
        }
    }
}
