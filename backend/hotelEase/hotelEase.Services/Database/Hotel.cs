
using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Address { get; set; } = null!;

    public int CityId { get; set; }

    public int CountryId { get; set; }

    public int ManagerId { get; set; }

    public int StarRating { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
    public string? StateMachine { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual User Manager { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
