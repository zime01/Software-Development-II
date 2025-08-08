using EasyNetQ;
using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : BaseCRUDController<Model.Notification, NotificationsSearchObject, NotificationsUpsertRequest, NotificationsUpsertRequest>
    {

        private readonly IBus _bus;
        public IEmailService _emailService { get; set; }
        public NotificationsController(INotificationsService service, IBus bus, IEmailService emailService) : base(service)
        {
            _bus = bus;
            _emailService = emailService;
        }

        [HttpPost("rabbitmq")]
        public async Task<ActionResult> SendNotification([FromBody] Model.NotificationMessage message)
        {
            await _bus.PubSub.PublishAsync(message);
            return Ok("Notification sent to queue");
        }

        [HttpPost("rabbit-mq")]
        public Task SendAndStoreNotificationAsync([FromBody] Model.NotificationRequest request)
        {
            return (_service as INotificationsService).SendAndStoreNotificationAsync( request.Message, request.UserId);
            
        }


    }
}
