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

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UsersInsertRequest request)
        {
            try
            {
                var user = _usersService.Register(request);
                return Ok(new { success = true, message = "Registration successful! Please login." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("me")]
        public ActionResult<Model.User> Me()
        {
            
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

        [HttpPut("change_password/{id}")]
        public IActionResult Update(int id, [FromBody] UsersUpdateRequest request)
        {
            try
            {
                var user = _usersService.Update(id, request);
                return Ok(user); 
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); 
            }
        }
    }
}
