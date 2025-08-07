using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class RoomType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? DeletedTime { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
