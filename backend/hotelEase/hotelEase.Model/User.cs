using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace hotelEase.Model
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        

        public string? PhoneNumber { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }

        //public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

        //public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        //public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        //public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
