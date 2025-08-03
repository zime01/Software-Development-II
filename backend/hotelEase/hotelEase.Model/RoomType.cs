using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class RoomType
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
