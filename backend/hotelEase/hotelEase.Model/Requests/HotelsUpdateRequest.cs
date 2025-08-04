using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class HotelsUpdateRequest
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Address { get; set; } = null!;

        public int CityId { get; set; }

        public int CountryId { get; set; }

        public int ManagerId { get; set; }

        public int StarRating { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
