using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class PaymentsUpsertRequest
    {
        public int ReservationId { get; set; }
        public string Provider { get; set; } = "stripe";
        public string ProviderPaymentId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string Status { get; set; } = "processing";
    }
}
