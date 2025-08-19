using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class Payment
{
    public int Id { get; set; }

    public int ReservationId { get; set; }

    public string Provider { get; set; } = null!;

    public string ProviderPaymentId { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;
}
