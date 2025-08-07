using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class Room
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public int RoomTypeId { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal PricePerNight { get; set; }

    public bool? IsAvailable { get; set; }

    public string? Description { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? DeletedTime { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<RoomAvailability> RoomAvailabilities { get; set; } = new List<RoomAvailability>();

    public virtual RoomType RoomType { get; set; } = null!;
}
