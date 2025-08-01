using hotelEase.Model;
using hotelEase.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class HotelsService : IHotelsService
    {
        public HotelEaseContext Context { get; set; }
        public HotelsService(HotelEaseContext context)
        {
            Context = context;
        }
        public List<Model.Hotel> List = new List<Model.Hotel>()
        {
            new Model.Hotel()
            {
                HotelId = 1,
                Name = "Hilton",
                Price = 149,
            },
            new Model.Hotel()
            {
                HotelId = 2,
                Name = "Swisotel",
                Price = 130,
            }

        };
        public List<Model.Hotel> GetList()
        {
            var list = Context.Hotels.ToList();
            var result  = new List<Model.Hotel>();
            foreach (var item in list)
            {
                result.Add(new Model.Hotel() {
                    HotelId = item.Id,
                    Name = item.Name,
                    
                });
            }

            return result;
        }
    }
}
