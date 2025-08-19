using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class PaymentsStatusUpdateRequest
    {
        public int PaymentId { get; set; }
        public string NewStatus { get; set; } = "succeeded"; // or "failed", "canceled"...
        public string? ProviderPaymentId { get; set; }
    }
}
