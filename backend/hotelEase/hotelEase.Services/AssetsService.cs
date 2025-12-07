using hotelEase.Model.Requests;
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
    public class AssetsService : BaseCRUDService<Model.Asset, AssetsSearchObject, Database.Asset, AssetsUpsertRequest, AssetsUpsertRequest>, IAssetsService
    {
        public AssetsService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Asset> AddFilter(AssetsSearchObject search, IQueryable<Asset> query)
        {
            var filteredQuery =  base.AddFilter(search, query);


            if(search?.HotelId.HasValue ==  true)
            {
                filteredQuery = filteredQuery.Where(a => a.HotelId == search.HotelId);
            }

            if(search?.RoomId.HasValue == true)
            {
                filteredQuery = filteredQuery.Where(a=>a.RoomId  == search.RoomId);
            }

            return filteredQuery;
        }

        


    }
}
