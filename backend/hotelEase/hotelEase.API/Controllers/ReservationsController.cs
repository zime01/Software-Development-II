using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : BaseCRUDController<Model.Reservation, ReservationsSearchObject, ReservationsUpsertRequest, ReservationsUpsertRequest>
    {
        public ReservationsController(IReservationsService service) : base(service) { }

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
    }
}
