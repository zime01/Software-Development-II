using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int HotelId { get; set; }

    public int ReservationId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? ReviewDate { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
