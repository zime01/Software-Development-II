using System;
using System.Collections.Generic;

namespace hotelEase.Services.Database;

public partial class Asset
{
    public int Id { get; set; }

    public string FileName { get; set; } = null!;

    public byte[]? Image { get; set; }

    public byte[]? ImageThumb { get; set; }

    public string? MimeType { get; set; }

    public DateTime CreatedAt { get; set; }

    public int HotelId { get; set; }

    public int RoomId { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
