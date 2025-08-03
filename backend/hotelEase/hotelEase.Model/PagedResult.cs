using System;
using System.Collections.Generic;
using System.Text;

namespace hotelEase.Model
{
    public class PagedResult<T>
    {
        public int? Count { get; set; }
        public List<T> ResultList { get; set; }
    }
}
