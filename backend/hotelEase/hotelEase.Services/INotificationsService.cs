using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface INotificationsService : ICRUDService<Model.Notification, NotificationsSearchObject, NotificationsUpsertRequest, NotificationsUpsertRequest>
    {
        Task SendAndStoreNotificationAsync(NotificationMessage message, int userId);
    }
}
