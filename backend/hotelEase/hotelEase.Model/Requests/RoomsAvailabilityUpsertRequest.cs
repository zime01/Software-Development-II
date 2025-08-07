using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class RoomsAvailabilityUpsertRequest
    {
        public int RoomId { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
