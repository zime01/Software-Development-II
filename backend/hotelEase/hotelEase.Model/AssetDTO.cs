using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class AssetDTO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string? Image { get; set; }     
        public string? ImageThumb { get; set; }
    }
}
