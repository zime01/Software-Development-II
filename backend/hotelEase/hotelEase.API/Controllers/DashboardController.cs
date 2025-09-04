using hotelEase.Model;
using hotelEase.Services;
using hotelEase.Services.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace hotelEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IUsersService _usersService;
        private readonly HotelEaseContext _context;

        public DashboardController(IDashboardService dashboardService, IUsersService usersService, HotelEaseContext context)
        {
            _dashboardService = dashboardService;
            _usersService = usersService;
            _context = context;
        }

        [HttpGet("manager/{hotelId}")]
        public ActionResult<ManagerDashboardDTO> GetDashboard(int hotelId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var managerEntity = _context.Users.FirstOrDefault(u => u.Username == username);
            if (managerEntity == null)
                return Unauthorized();

            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null)
                return Forbid();
            var data = _dashboardService.GetManagerDashboard(hotelId);

            var managerDTO = _usersService.GetCurrentUser(username);

            return Ok(new
            {
                Manager = managerDTO,
                Dashboard = data
            });
        }
    }
}
