using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using hotelEase.Services.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    public class RoomsController : BaseCRUDController<Model.Room, RoomsSearchObject, RoomsInsertRequest, RoomsUpdateRequest>
    {
        private readonly IRoomsService _roomsService;
        private readonly IUsersService _usersService;
        private readonly HotelEaseContext _context;

        public RoomsController(IRoomsService service, IUsersService usersService, HotelEaseContext context) : base(service) {

            _roomsService = service;
            _usersService = usersService;
            _context = context;
        }

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

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public override ActionResult<Model.Room> Insert([FromBody] RoomsInsertRequest request)
        {
            // Ako je manager, provjeri da li je manager tog hotela
            var username = HttpContext.User.Identity?.Name;
            var user = _usersService.GetCurrentUser(username);
            if (User.IsInRole("Manager"))
            {
                var hotel = _context.Hotels.FirstOrDefault(h => h.Id == request.HotelId);
                if (hotel == null || hotel.ManagerId != user.Id)
                    return Forbid(); // zabranjeno dodavanje sobe u tuđi hotel
            }

            return base.Insert(request);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public override ActionResult<Model.Room> Update(int id, [FromBody] RoomsUpdateRequest request)
        {
            var username = HttpContext.User.Identity?.Name;
            var user = _usersService.GetCurrentUser(username);
            if (User.IsInRole("Manager"))
            {
                var room = _roomsService.GetById(id);
                var hotel = _context.Hotels.FirstOrDefault(h => h.Id == room.HotelId);
                if (room == null || hotel == null || hotel.ManagerId != user.Id)
                    return Forbid();
            }

            return base.Update(id, request);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public override ActionResult<Model.Room> Delete(int id)
        {
            var username = HttpContext.User.Identity?.Name;
            var user = _usersService.GetCurrentUser(username);

            if (User.IsInRole("Manager"))
            {
                var room = _roomsService.GetById(id);
                var hotel = _context.Hotels.FirstOrDefault(h => h.Id == room.HotelId);

                if (room == null || hotel == null || hotel.ManagerId != user.Id)
                    return Forbid();
            }

            return base.Delete(id);
        }
    }



}
