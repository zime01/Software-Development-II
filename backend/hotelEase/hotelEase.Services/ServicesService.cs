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
    public class ServicesService : BaseCRUDService<Model.Service, ServicesSearchObject, Database.Service, ServicesUpsertObject, ServicesUpsertObject>, IServicesService
    {
        public ServicesService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<Model.Service> GetByHotelId(int hotelId)
        {
            var query = Context.Services.Where(x=>x.HotelId == hotelId && (x.IsDeleted == null || x.IsDeleted == false)).AsQueryable();

            var list = query.ToList();

            return Mapper.Map<List<Model.Service>>(list);
        }
    }
}
