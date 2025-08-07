using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : BaseCRUDController<Model.Review, ReviewsSearchObject, ReviewsUpsertRequest, ReviewsUpsertRequest>
    {
        public ReviewsController(IReviewsService service) : base(service) { }

        [HttpGet("by-hotel/{hotelId}")]
        public List<Model.Review> GetByHotelId(int hotelId)
        {
            return (_service as IReviewsService).GetByHotelId(hotelId);
        }
    }
}
