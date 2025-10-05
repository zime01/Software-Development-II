using hotelEase.Model;
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

        public override IQueryable<Database.Room> AddInclude(RoomsSearchObject search, IQueryable<Database.Room> query)
        {
            if (search.IsAssetsIncluded == true)
            {
                query = query.Include(x=>x.Assets);
            }

            if (search.IsHotelIncluded == true)
            {
                query = query.Include(x => x.Hotel);
            }


            return query;
        }

        public override Model.Room GetById(int id)
        {
            var room = Context.Rooms
                .Include(r => r.Assets)
                .Include(r => r.Hotel)
                .FirstOrDefault(r => r.Id == id);

            if (room == null)
                return null;

            return Mapper.Map<Model.Room>(room);
        }



        public List<Model.Room> GetRoomByHotel(int hotelId)
        {
            var query = Context.Rooms.Include(x=>x.Assets).AsQueryable();

            if(typeof(Database.Room).GetProperty("IsDeleted") != null)
            {
                query = query.Where(r => EF.Property<bool?>(r, "IsDeleted") == false || EF.Property<bool?>(r, "IsDeleted") == null);
            }

            var filtered = query.Where(x=>x.HotelId == hotelId).ToList();

            return Mapper.Map<List<Model.Room>>(filtered);
        }

        public Model.RoomDTO GetRoomDtoById(int id)
        {
            var room = Context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefault(r => r.Id == id);

            if (room == null)
                return null;

            return new Model.RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable,
                Description = room.Description,
                HotelName = room.Hotel?.Name
            };
        }

    }
}
