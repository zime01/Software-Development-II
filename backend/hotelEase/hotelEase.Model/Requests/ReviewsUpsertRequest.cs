using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class ReviewsUpsertRequest
    {
        

        public int UserId { get; set; }

        public int HotelId { get; set; }

        public int ReservationId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime? ReviewDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
