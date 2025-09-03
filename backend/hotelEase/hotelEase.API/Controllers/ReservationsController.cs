using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using hotelEase.Services.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : BaseCRUDController<Model.Reservation, ReservationsSearchObject, ReservationsUpsertRequest, ReservationsUpsertRequest>
    {
        private readonly HotelEaseContext _context;
        private readonly IUsersService _usersService;
        public ReservationsController(IReservationsService service, HotelEaseContext context, IUsersService usersService ) : base(service) { 
            _context = context;
            _usersService = usersService;
        }

        [HttpGet("user/{userId}")]
        public List<Model.Reservation> GetByUserId(int userId)
        {
            return (_service as IReservationsService).GetByUserId(userId);
        }

        [HttpPut("update-status")]
        public Task<Model.Reservation> UpdateStatusAsync([FromBody] ReservationsStatusUpdateRequest request)
        {
            return (_service as IReservationsService).UpdateStatusAsync(request.ReservationId, request.NewStatus, request.ActingUserId);
            
        }

        [HttpPost("insert-reservation")]
        public async Task<Model.Reservation> Insert(ReservationsUpsertRequest request)
        {
            return await (_service as IReservationsService).InsertAsync(request);
        }

        [HttpGet("manager/hotel/{hotelId}")]
        [Authorize(Roles = "Manager")]
        public IActionResult GetReservationsForHotel(int hotelId)
        {
            var username = HttpContext.User.Identity?.Name;
            var manager = _usersService.GetCurrentUser(username);

            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == hotelId && h.ManagerId == manager.Id);
            if (hotel == null) return Forbid();

            var reservations = (_service as IReservationsService).GetReservationsByHotel(hotelId);
            return Ok(reservations);
        }

        [HttpPut("manager/update-status")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateReservationStatus([FromBody] ReservationsStatusUpdateRequest request)
        {
            var username = HttpContext.User.Identity?.Name;
            var manager = _usersService.GetCurrentUser(username);

            var reservation = (_service as IReservationsService).GetById(request.ReservationId);
            if (reservation == null) return NotFound();

            var hotel = _context.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId)?.Hotel;
            if (hotel == null || hotel.ManagerId != manager.Id) return Forbid();

            var updated = await (_service as IReservationsService).UpdateStatusAsync(
                request.ReservationId, request.NewStatus, manager.Id
            );

            return Ok(updated);
        }

    }
}
