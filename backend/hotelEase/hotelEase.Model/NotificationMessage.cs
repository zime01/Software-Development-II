using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class NotificationMessage
    {
        public string Type { get; set; } // email, push
        public string To { get; set; } // Email adresa ili UserID
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
