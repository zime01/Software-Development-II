using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace hotelEase.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public string Address { get; set; } = null!;

        public int CityId { get; set; }

        public int CountryId { get; set; }

        public int ManagerId { get; set; }

        public int StarRating { get; set; }

        public bool? IsActive { get; set; }
        public bool? SPA { get; set; }
        public bool? Parking { get; set; }
        public bool? WiFi { get; set; }
        public bool? Pool { get; set; }
        public bool? Bar { get; set; }
        public bool? Fitness { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? StateMachine { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
        public virtual City City { get; set; } = null!;

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

    }
}
