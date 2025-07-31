using hotelEase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IHotelsService
    {
        List<Hotels> GetList();
    }
}
