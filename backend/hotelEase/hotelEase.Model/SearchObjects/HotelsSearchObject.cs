using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.SearchObjects
{
    public class HotelsSearchObject : BaseSearchObject
    {
        public string? FTS { get; set; }
        public bool? IsRoomsIcluded { get; set; }
        
        public string? OrderBy { get; set; }

        public string? CityName { get; set; }

        public int? Adults { get; set; }

        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? RoomsCount { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinStarRating { get; set; }
        // Amenities
        public bool? WiFi { get; set; }
        public bool? Parking { get; set; }
        public bool? Pool { get; set; }
        public bool? Bar { get; set; }
        public bool? Fitness { get; set; }
        public bool? SPA { get; set; }
        // Sort
        public string? SortBy { get; set; } // PriceAsc, PriceDesc, StarRatingDesc...
        public string? FilterBy { get; set; } // npr. "wifi", "parking", "pool"
    }
}
