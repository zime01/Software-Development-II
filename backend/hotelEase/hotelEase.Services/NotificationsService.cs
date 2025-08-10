using EasyNetQ;
using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class NotificationsService : BaseCRUDService<Model.Notification, NotificationsSearchObject, Database.Notification, NotificationsUpsertRequest, NotificationsUpsertRequest>, INotificationsService
    {
        private readonly IBus _bus;
        public NotificationsService(HotelEaseContext context, IMapper mapper, IBus bus) : base(context, mapper)
        {
            _bus = bus;
        }

        public async Task SendAndStoreNotificationAsync(NotificationMessage message, int userId)    
        {
            var dbNotification = new Database.Notification
            {
                UserId = userId,
                Title = message.Subject,
                Message = message.Body,
                Type = message.Type,
                IsRead = false,
                SentAt = DateTime.Now
            };

            Context.Notifications.Add(dbNotification);

            await Context.SaveChangesAsync();

            await _bus.PubSub.PublishAsync(message);

            //if (paymentSucces)
            //{
            //    var message = new NotificationMessage
            //    {
            //        Type = "email",
            //        To = user.Email,
            //        Subject = "Reservation Confirmed",
            //        Body = $"Your reservation #{reservation.Id} at {reservation.Hotel.Name} is confirmed. Check-in: {reservation.CheckInDate:yyyy-MM-dd}."
            //    };

            //    await _notificationsService.SendAndStoreNotificationAsync(message, user.Id);
            //}

            //if (!paymentSuccess)
            //{
            //    var msg = new NotificationMessage
            //    {
            //        Type = "email",
            //        To = user.Email,
            //        Subject = "Payment Failed",
            //        Body = $"Your payment for reservation attempt failed. Please try again or contact support."
            //    };
            //    await _notificationsService.SendAndStoreNotificationAsync(msg, user.Id);
            //}
        }
    }
}
