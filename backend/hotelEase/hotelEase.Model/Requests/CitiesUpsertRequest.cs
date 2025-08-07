using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model.Requests
{
    public class CitiesUpsertRequest
    {
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }
    }
}
