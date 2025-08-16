using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.SearchObjects
{
    public class AssetsSearchObject : BaseSearchObject
    {
        public int? HotelId { get; set; }
        public int? RoomId { get; set; }
    }
}
