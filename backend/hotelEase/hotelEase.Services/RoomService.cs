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
        public RoomService(HotelEaseContext context, IMapper mapper) : base(context, mapper) { }

        public override IQueryable<Database.Room> AddInclude(RoomsSearchObject search, IQueryable<Database.Room> query)
        {
            if (search.IsAssetsIncluded == true)
                query = query.Include(x => x.Assets);

            if (search.IsHotelIncluded == true)
                query = query.Include(x => x.Hotel);

            return query;
        }

        public override Model.Room GetById(int id)
        {
            var room = Context.Rooms
                .Include(r => r.Assets)
                .Include(r => r.Hotel)
                .FirstOrDefault(r => r.Id == id);

            return room == null ? null : Mapper.Map<Model.Room>(room);
        }

        public List<Model.Room> GetRoomByHotel(int hotelId)
        {
            var query = Context.Rooms.Include(x => x.Assets).AsQueryable();

            if (typeof(Database.Room).GetProperty("IsDeleted") != null)
                query = query.Where(r => EF.Property<bool?>(r, "IsDeleted") == false || EF.Property<bool?>(r, "IsDeleted") == null);

            return Mapper.Map<List<Model.Room>>(query.Where(x => x.HotelId == hotelId).ToList());
        }

        public Model.RoomDTO GetRoomDtoById(int id)
        {
            var room = Context.Rooms.Include(r => r.Hotel).FirstOrDefault(r => r.Id == id);
            if (room == null) return null;

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

        private void ValidateAsset(Model.Asset asset)
        {
            var allowedTypes = new List<string> { "image/jpeg", "image/png" };
            if (!allowedTypes.Contains(asset.MimeType))
                throw new Exception("Unsupported image type.");

            long maxSize = 5 * 1024 * 1024; // 5MB
            if (asset.Image != null && asset.Image.Length > maxSize)
                throw new Exception("Image too large.");
        }

        public override Model.Room Insert(RoomsInsertRequest request)
        {
            var room = base.Insert(request);

            if (request.Assets != null)
            {
                foreach (var asset in request.Assets)
                {
                    ValidateAsset(asset);
                    asset.RoomId = room.Id;
                    Context.Assets.Add(Mapper.Map<Database.Asset>(asset));
                }
                Context.SaveChanges();
            }

            return room;
        }

        public override Model.Room Update(int id, RoomsUpdateRequest request)
        {
            var room = base.Update(id, request);

            if (request.Assets != null)
            {
                foreach (var asset in request.Assets)
                {
                    ValidateAsset(asset);

                    if (asset.Id > 0)
                    {
                        var existing = Context.Assets.Find(asset.Id);
                        if (existing != null)
                            Mapper.Map(asset, existing);
                    }
                    else
                    {
                        asset.RoomId = room.Id;
                        Context.Assets.Add(Mapper.Map<Database.Asset>(asset));
                    }
                }
                Context.SaveChanges();
            }

            return room;
        }

        // Novi helper za soft delete više assets
        public void DeleteAssets(List<int> assetIds)
        {
            foreach (var id in assetIds)
            {
                var asset = Context.Assets.Find(id);
                if (asset != null)
                {
                    asset.IsDeleted = true;
                    asset.DeletedTime = DateTime.Now;
                }
            }
            Context.SaveChanges();
        }
    }
}
