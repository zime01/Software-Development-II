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
    public class RolesService : BaseCRUDService<Model.Role, RolesSearchObject, Database.Role, RolesUpsertRequest, RolesUpsertRequest>, IRolesService
    {
        public RolesService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
