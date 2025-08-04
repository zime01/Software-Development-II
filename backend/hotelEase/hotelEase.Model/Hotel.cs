using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace hotelEase.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? StateMachine { get; set; }


        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
