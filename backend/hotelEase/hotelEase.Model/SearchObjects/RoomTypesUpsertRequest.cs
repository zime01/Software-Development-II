using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.SearchObjects
{
    public class RoomTypesUpsertRequest
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
