using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class PaymentsCreateIntentRequest
    {
        public int ReservationId { get; set; }
        public string Provider { get; set; } = "stripe";
        public string Currency { get; set; } = "USD";
        // Ako hoćeš da server izračuna amount iz rezervacije, ne šalji Amount.
        public decimal? OverrideAmount { get; set; }
    }
}
