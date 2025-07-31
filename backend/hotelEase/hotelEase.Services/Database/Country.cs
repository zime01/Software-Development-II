using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
