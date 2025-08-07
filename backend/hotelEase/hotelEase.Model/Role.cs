using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
