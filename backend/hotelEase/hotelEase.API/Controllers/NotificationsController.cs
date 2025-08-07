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
        public NotificationsController(INotificationsService service) : base(service) { }
    }
}
