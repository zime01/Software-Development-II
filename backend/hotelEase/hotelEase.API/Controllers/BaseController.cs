using hotelEase.Model;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController< TModel, TSearch> : ControllerBase where TSearch : BaseSearchObject
    {
        protected IService<TModel, TSearch> _service;
        public BaseController(IService<TModel, TSearch> service)
        {
            _service = service;
        }

        [HttpGet]
        public Model.PagedResult<TModel> GetPaged([FromQuery] TSearch searchObject)
        {
            return _service.GetPaged(searchObject);
        }
        [HttpGet("{id}")]
        public TModel GetById(int id)
        {
            return _service.GetById(id);
        }
    }
}
