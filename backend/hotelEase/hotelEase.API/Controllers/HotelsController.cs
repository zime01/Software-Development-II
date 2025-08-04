using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : BaseCRUDController<Model.Hotel, HotelsSearchObject, HotelsInsertRequest, HotelsUpdateRequest>
    {
        public HotelsController(IHotelsService service) : base(service) { }

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
    }
}
