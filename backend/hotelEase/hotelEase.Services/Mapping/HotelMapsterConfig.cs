using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace hotelEase.Services.Mapping
{
    public class HotelMapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<hotelEase.Services.Database.Hotel, hotelEase.Model.Hotel>()
                .Map(dest => dest.Price,
                     src => src.Rooms.Any()
                         ? src.Rooms.Average(r => r.PricePerNight)
                         : 0)
                .Map(dest => dest.MinPrice,
                     src => src.Rooms.Any()
                         ? src.Rooms.Min(r => r.PricePerNight)
                         : 0)
                .Map(dest => dest.MaxPrice,
                     src => src.Rooms.Any()
                         ? src.Rooms.Max(r => r.PricePerNight)
                         : 0)
                .Map(dest => dest.City, src => src.City);
        }
    }
}
