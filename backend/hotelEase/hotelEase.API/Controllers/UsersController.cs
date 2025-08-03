using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        protected IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public List<Model.User> GetList([FromQuery] UsersSearchObject searchObject)
        {
            return _usersService.GetList( searchObject );
        }

        [HttpPost]
        public Model.User Insert(UsersInsertRequest request)
        {
            return _usersService.Insert(request);
        }

        [HttpPut("{id}")]
        public Model.User Update(int id, UsersUpdateRequest request)
        {
            return _usersService.Update(id, request);
        }
    }
}
