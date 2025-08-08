using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class NotificationRequest
    {
        public NotificationMessage Message { get; set; }
        public int UserId { get; set; }
    }
}
