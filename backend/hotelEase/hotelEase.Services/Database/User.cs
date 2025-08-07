using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? DeletedTime { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
