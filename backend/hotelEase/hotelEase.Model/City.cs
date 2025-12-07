using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
