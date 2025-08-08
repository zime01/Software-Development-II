using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class ReservationsStatusUpdateRequest
    {
        public int ReservationId { get; set; }
        public string NewStatus { get; set; }
        public int ActingUserId { get; set; }
        
    }
}
