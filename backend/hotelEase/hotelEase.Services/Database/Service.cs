using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int HotelId { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<ReservationService> ReservationServices { get; set; } = new List<ReservationService>();
}
