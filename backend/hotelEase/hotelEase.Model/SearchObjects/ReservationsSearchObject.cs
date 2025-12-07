using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.SearchObjects
{
    public class ReservationsSearchObject : BaseSearchObject
    {
        public DateTime? Date { get; set; }
        public int? HotelId { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; }
    }
}
