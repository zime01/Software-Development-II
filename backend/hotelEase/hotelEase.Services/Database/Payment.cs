using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services.Database
{
    public class Payment
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        // "stripe" | "paypal" | ...
        public string Provider { get; set; } = "stripe";

        // npr. Stripe PaymentIntent id: "pi_..."
        public string ProviderPaymentId { get; set; } = string.Empty;

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";

        // "requires_payment_method" | "requires_confirmation" | "processing" | "succeeded" | "canceled" | "failed"...
        public string Status { get; set; } = "processing";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }

        public virtual Reservation Reservation { get; set; } = null!;
    }
}

