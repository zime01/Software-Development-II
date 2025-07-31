using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class Reservation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string? Statsu { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ReservationService> ReservationServices { get; set; } = new List<ReservationService>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
