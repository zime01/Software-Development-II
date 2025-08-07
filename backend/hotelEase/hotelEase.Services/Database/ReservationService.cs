using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class ReservationService
{
    public int Id { get; set; }

    public int ReservationId { get; set; }

    public int ServiceId { get; set; }

    public decimal Price { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? DeletedTime { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
