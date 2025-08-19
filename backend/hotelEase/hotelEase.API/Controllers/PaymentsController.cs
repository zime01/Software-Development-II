using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : BaseCRUDController<Model.Payment, PaymentsSearchObject, PaymentsUpsertRequest, PaymentsUpsertRequest>
    {
        private readonly IPaymentsService _payments;

        public PaymentsController(IPaymentsService service) : base(service)
        {
            _payments = service;
        }

        
        [HttpPost("create-intent")]
        public async Task<ActionResult<Model.Payment>> CreateIntent([FromBody] PaymentsCreateIntentRequest request)
        {
            var result = await _payments.CreateIntentAsync(request);
            return Ok(result);
        }

        [HttpPost("update-status")]
        public async Task<ActionResult<Model.Payment>> UpdateStatus([FromBody] PaymentsStatusUpdateRequest request)
        {
            var result = await _payments.UpdateStatusAsync(request);
            return Ok(result);
        }

        [HttpGet("by-reservation/{reservationId}")]
        public ActionResult<List<Model.Payment>> GetByReservation(int reservationId)
        {
            var list = _payments.GetByReservation(reservationId);
            return Ok(list);
        }

        
        [HttpPost("stripe-create-intent")]
        public async Task<IActionResult> StripeCreateIntent([FromBody] CreatePaymentIntentRequest request)
        {
            var p = await _payments.CreateStripePaymentIntentAsync(
                request.ReservationId,
                request.Amount,
                request.Currency
            );

            return Ok(new
            {
                paymentId = p.Id,                   
                paymentIntentId = p.ProviderPaymentId, 
                clientSecret = p.ClientSecret         
            });
        }

        
        [HttpPost("stripe-update-status/{paymentIntentId}")]
        public async Task<IActionResult> StripeUpdateStatus(string paymentIntentId)
        {
            await _payments.UpdateStripePaymentStatusAsync(paymentIntentId);
            return Ok(new { updated = true });
        }
    }
}
