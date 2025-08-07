using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class RoomAvailability
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
