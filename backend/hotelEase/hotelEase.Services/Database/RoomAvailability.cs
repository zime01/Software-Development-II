using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class RoomAvailability
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public DateTime Date { get; set; }

    public int Status { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public virtual Room Room { get; set; } = null!;
}
