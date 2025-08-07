﻿using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class RoomsInsertRequest
    {
        

        public int HotelId { get; set; }

        public int RoomTypeId { get; set; }

        public string Name { get; set; } = null!;

        public int Capacity { get; set; }

        public decimal PricePerNight { get; set; }

        public bool? IsAvailable { get; set; }

        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
