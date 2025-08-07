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
    public class ReservationsService : BaseCRUDService<Model.Reservation, ReservationsSearchObject, Database.Reservation, ReservationsUpsertRequest, ReservationsUpsertRequest>, IReservationsService
    {
        public ReservationsService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<Model.Reservation> GetByUserId(int id)
        {
            var query = Context.Reservations.Where(x=>x.UserId == id && (x.IsDeleted == null || x.IsDeleted == false)).AsQueryable();

            var list = query.ToList();

            return Mapper.Map<List<Model.Reservation>>(list);
        }
    }
}
