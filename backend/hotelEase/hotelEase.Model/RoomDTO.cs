using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool? IsAvailable { get; set; }
        public string? Description { get; set; }

        
        public string? HotelName { get; set; }
    }
}
