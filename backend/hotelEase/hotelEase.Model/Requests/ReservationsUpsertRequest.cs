using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class ReservationsUpsertRequest
    {
        

        public int UserId { get; set; }

        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
