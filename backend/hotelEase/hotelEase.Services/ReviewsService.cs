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
    public class ReviewsService : BaseCRUDService<Model.Review, ReviewsSearchObject, Database.Review, ReviewsUpsertRequest, ReviewsUpsertRequest>, IReviewsService
    {
        public ReviewsService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<Model.Review> GetByHotelId(int hotelId)
        {
            var query = Context.Reviews.Where(x=>x.HotelId == hotelId && (x.IsDeleted == null || x.IsDeleted == false)).AsQueryable();

            var list = query.ToList();

            return Mapper.Map<List<Model.Review>>(list);
        }
    }
}
