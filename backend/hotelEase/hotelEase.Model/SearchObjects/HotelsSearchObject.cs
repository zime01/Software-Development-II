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
    }
}
