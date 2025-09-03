using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.SearchObjects
{
    public class UsersSearchObject : BaseSearchObject
    {
        public string? FirstNameGTE { get; set; }
        public string? LastNameGTE { get; set; }

        public string? Email { get; set; }
        public string? Username { get; set; }
        public bool IncludeRoles { get; set; }


    }
}
