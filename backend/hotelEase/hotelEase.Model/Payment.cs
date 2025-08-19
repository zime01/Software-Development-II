using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public string Provider { get; set; } = "stripe";
        public string ProviderPaymentId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string Status { get; set; } = "processing";
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Reservation Reservation { get; set; } = null!;
        public string? ClientSecret { get; set; }
    }
}
