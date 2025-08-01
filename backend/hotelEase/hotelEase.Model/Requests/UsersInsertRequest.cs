using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class UsersInsertRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }
    }
}
