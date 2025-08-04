using hotelEase.Model;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class RoomTypesService : BaseCRUDService<Model.RoomType, RoomTypesSearchObject, Database.RoomType, RoomTypesUpsertRequest, RoomTypesUpsertRequest> ,IRoomTypesService
    {

        public RoomTypesService(HotelEaseContext context, IMapper mapper): base(context, mapper) { }
        //public HotelEaseContext Context { get; set; }
        //public IMapper Mapper { get; set; }
        //public RoomTypesService(HotelEaseContext context, IMapper mapper)
        //{
        //    Context = context;
        //    Mapper = mapper;
        //}
        //public List<Model.RoomType> GetList(RoomTypesSearchObject searchObject)
        //{
        //    List<Model.RoomType> result = new List<Model.RoomType>();

        //    var query = Context.RoomTypes.AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(searchObject?.NameGTE))
        //    {
        //        query = query.Where(x=> x.Name.Contains(searchObject.NameGTE));
        //    }

        //    if(searchObject?.Page.HasValue == true && searchObject?.PageSize.HasValue == true)
        //    {
        //        query = query.Skip(searchObject.Page.Value * searchObject.PageSize.Value).Take(searchObject.PageSize.Value);
        //    }

        //    var list = query.ToList();

        //    result = Mapper.Map(list, result);

        //    return result;
        //}
    }
}
