using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq.Dynamic.Core;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseCRUDController<Model.User, UsersSearchObject, UsersInsertRequest, UsersUpdateRequest>
    {
        protected IUsersService _usersService;

        public UsersController(IUsersService usersService):base(usersService)
        {

        }
        [AllowAnonymous]
        [HttpPost("login")]
        public Model.User Login(string username, string password)
        {
            return (_service as IUsersService).Login(username, password); 
        }
        

        //[HttpGet]
        //public Model.PagedResult<Model.User> GetList([FromQuery] UsersSearchObject searchObject)
        //{
        //    return _usersService.GetPaged( searchObject );
        //}

        //[HttpPost]
        //public Model.User Insert(UsersInsertRequest request)
        //{
        //    return _usersService.Insert(request);
        //}

        //[HttpPut("{id}")]
        //public Model.User Update(int id, UsersUpdateRequest request)
        //{
        //    return _usersService.Update(id, request);
        //}
    }
}
