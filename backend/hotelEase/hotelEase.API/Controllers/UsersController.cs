using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseCRUDController<Model.User, UsersSearchObject, UsersInsertRequest, UsersUpdateRequest>
    {
        protected IUsersService _usersService;

        public UsersController(IUsersService usersService) : base(usersService)
        {
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public Model.User Login(string username, string password)
        {
            return (_service as IUsersService).Login(username, password);
        }

        [HttpGet("me")]
        public ActionResult<Model.User> Me()
        {
            // Izvući Authorization header
            var authHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
                return Unauthorized();

            try
            {
                var encoded = authHeader.Substring("Basic ".Length).Trim();
                var decodedBytes = Convert.FromBase64String(encoded);
                var decoded = Encoding.UTF8.GetString(decodedBytes);

                var parts = decoded.Split(':');
                if (parts.Length != 2)
                    return Unauthorized();

                var username = parts[0];
                var user = (_service as IUsersService).GetCurrentUser(username);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
