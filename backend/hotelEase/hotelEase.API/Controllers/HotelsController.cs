using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : BaseCRUDController<Model.Hotel, HotelsSearchObject, HotelsInsertRequest, HotelsUpdateRequest>
    {
        public HotelRecommenderService _hotelRecommender { get; set; }
        public HotelsController(IHotelsService service, HotelRecommenderService hotelRecommenderService) : base(service) {
             _hotelRecommender = hotelRecommenderService;
        }

        [HttpPut("{id}/activate")]
        public Hotel Activate(int id)
        {
            return (_service as IHotelsService).Activate(id);
        }
        [HttpPut("{id}/edit")]
        public Hotel Edit(int id)
        {
            return (_service as IHotelsService).Edit(id);
        }
        [HttpPut("{id}/hide")]
        public Hotel Hide(int id)
        {
            return (_service as IHotelsService).Hide(id);
        }

        [HttpGet("{id}/allowedActions")]
        public List<string> AllowedActions(int id)
        {
            return (_service as IHotelsService).AllowedActions(id);
        }

        [HttpGet("popular")]
        public IActionResult GetPopular([FromQuery] int top = 5)
        {
            var result = _hotelRecommender.GetPopularHotels(top) ?? new List<Hotel>();
            return Ok(result.Any() ? result : new List<Hotel>());
        }

        [HttpGet("{hotelId}/content-based")]
        public IActionResult GetContentBased(int hotelId, [FromQuery] int top = 5)
        {
            var result = _hotelRecommender.GetContentBased(hotelId, top) ?? new List<Hotel>();
            return Ok(result.Any() ? result : new List<Hotel>());
        }

        [HttpGet("user/{userId}/collaborative")]
        public IActionResult GetCollaborative(int userId, [FromQuery] int top = 5)
        {
            var result = _hotelRecommender.GetCollaborativeFiltering(userId, top) ?? new List<Hotel>();
            return Ok(result.Any() ? result : new List<Hotel>());
        }
    }
}
