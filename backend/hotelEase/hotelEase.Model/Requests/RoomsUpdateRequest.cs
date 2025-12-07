using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class RoomsUpdateRequest
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }

        public string Name { get; set; } = null!;

        public int Capacity { get; set; }

        public decimal PricePerNight { get; set; }

        public bool? IsAvailable { get; set; }

        public string? Description { get; set; }
        public bool? QueenBed { get; set; }
        public bool? WiFi { get; set; }
        public bool? CityView { get; set; }
        public bool? AC { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
        public List<Asset>? Assets { get; set; }
    }
}
