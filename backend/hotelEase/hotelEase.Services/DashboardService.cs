using hotelEase.Model;
using hotelEase.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IDashboardService
    {
        ManagerDashboardDTO GetManagerDashboard(int hotelId);
    }

    public class DashboardService : IDashboardService
    {
        private readonly HotelEaseContext _context;
        private readonly IMapper _mapper;

        public DashboardService(HotelEaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ManagerDashboardDTO GetManagerDashboard(int hotelId)
        {
            var now = DateTime.Now;
            var firstDay = new DateTime(now.Year, now.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            var reservations = _context.Reservations
                .Include(r => r.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(r => r.User) 
                .Include(r => r.ReservationServices)
                    .ThenInclude(rs => rs.Service)
                .Where(r => r.Room.HotelId == hotelId && (r.IsDeleted == null || r.IsDeleted == false))
                .ToList();

            var dto = new ManagerDashboardDTO
            {
                TotalReservations = reservations.Count,
                ActiveReservations = reservations.Count(r => r.Status != null && r.Status.ToLower() == "active"),
                CancelledReservations = reservations.Count(r => r.Status != null && r.Status.ToLower() == "cancelled"),
                MonthlyRevenue = reservations
                    .Where(r => r.CheckInDate >= firstDay && r.CheckOutDate <= lastDay)
                    .Sum(r => r.TotalPrice),
                RoomOccupancy = reservations
                    .Where(r => r.Room != null) 
                    .GroupBy(r => r.Room.Name)
                    .Select(g => new RoomOccupancyDTO
                    {
                        RoomName = g.Key,
                        OccupancyRate = (double)g.Count() / DateTime.DaysInMonth(now.Year, now.Month)
                    })
                    .ToList(),
                TopServices = reservations
                    .SelectMany(r => r.ReservationServices)
                    .Where(rs => rs.Service != null) 
                    .GroupBy(rs => rs.Service.Name)
                    .Select(g => new ServiceUsageDTO
                    {
                        ServiceName = g.Key,
                        UsageCount = g.Count()
                    })
                    .OrderByDescending(x => x.UsageCount)
                    .Take(5)
                    .ToList()
            };

            dto.OccupancyRate = dto.RoomOccupancy.Any()
                ? dto.RoomOccupancy.Average(r => r.OccupancyRate) * 100
                : 0;

            dto.NewUsers = _context.Users
                .Count(u => u.CreatedAt >= DateTime.Now.AddMonths(-1));

            
            dto.ActiveReservations = reservations
                .Count(r => !string.IsNullOrEmpty(r.Status)
                         && r.Status.ToLower().Contains("active"));

            dto.RecentReservations = reservations
                .OrderByDescending(r => r.CreatedAt) 
                .Take(5)
                .Select(r => new RecentReservationDTO
                {
                    Id = r.Id,
                    Status = r.Status,
                    TotalPrice = r.TotalPrice,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    User = r.User != null ? new UserDTO
                    {
                        FirstName = r.User.FirstName,
                        LastName = r.User.LastName
                    } : null,
                    Room = r.Room != null ? new RoomDTo
                    {
                        Name = r.Room.Name
                    } : null
                }).ToList();

            return dto;
        }
    }
}
