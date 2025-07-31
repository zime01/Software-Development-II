using hotelEase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class HotelsService : IHotelsService
    {
        public List<Hotels> List = new List<Hotels>()
        {
            new Hotels()
            {
                HotelId = 1,
                Name = "Hilton",
                Price = 149,
            },
            new Hotels()
            {
                HotelId = 2,
                Name = "Swisotel",
                Price = 130,
            }

        };
        public List<Hotels> GetList()
        {
            return List;
        }
    }
}
