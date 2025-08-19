using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class Notification
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Message { get; set; }

    public string Type { get; set; } = null!;

    public bool? IsRead { get; set; }

    public DateTime? SentAt { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public virtual User User { get; set; } = null!;
}
