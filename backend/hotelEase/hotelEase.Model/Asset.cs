using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class Asset
    {
        public int Id { get; set; }

        public string FileName { get; set; } = null!;

        public byte[]? Image { get; set; }

        public byte[]? ImageThumb { get; set; }

        public string? MimeType { get; set; }

        public DateTime CreatedAt { get; set; }

        public int HotelId { get; set; }

        public int RoomId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
