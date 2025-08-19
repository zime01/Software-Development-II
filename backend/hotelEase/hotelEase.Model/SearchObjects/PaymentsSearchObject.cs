using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.SearchObjects
{
    public class PaymentsSearchObject : BaseSearchObject
    {
        public int? ReservationId { get; set; }
        public string? Status { get; set; }
        public string? Provider { get; set; }
    }
}
