using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class RoomService : BaseCRUDService<Model.Room, RoomsSearchObject, Database.Room, RoomsInsertRequest, RoomsUpdateRequest>, IRoomsService
    {
        public RoomService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {

            
        }

        public override IQueryable<Room> AddInclude(RoomsSearchObject search, IQueryable<Room> query)
        {
            if (search.IsAssetsIncluded == true)
            {
                query = query.Include(x=>x.Assets);
            }


            return query;
        }

        public List<Model.Room> GetRoomByHotel(int hotelId)
        {
            var query = Context.Rooms.Include(x=>x.Assets).AsQueryable();

            if(typeof(Room).GetProperty("IsDeleted") != null)
            {
                query = query.Where(r => EF.Property<bool?>(r, "IsDeleted") == false || EF.Property<bool?>(r, "IsDeleted") == null);
            }

            var filtered = query.Where(x=>x.HotelId == hotelId).ToList();

            return Mapper.Map<List<Model.Room>>(filtered);
        }
    }
}
