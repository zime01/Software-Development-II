using System;
using System.Collections.Generic;

namespace hotelEase.Model
{
    public class ManagerDashboardDTO
    {
        public int TotalReservations { get; set; }
        public int ActiveReservations { get; set; }
        public int CancelledReservations { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public List<RoomOccupancyDTO> RoomOccupancy { get; set; } = new List<RoomOccupancyDTO>();
        public List<ServiceUsageDTO> TopServices { get; set; } = new List<ServiceUsageDTO>();

        public int NewUsers { get; set; }
        public double OccupancyRate { get; set; }

        public List<RecentReservationDTO> RecentReservations { get; set; } = new List<RecentReservationDTO>();
    }

    public class RecentReservationDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public UserDTO User { get; set; }
        public RoomDTo Room { get; set; }
    }
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RoomOccupancyDTO
    {
        public string RoomName { get; set; }
        public double OccupancyRate { get; set; }
    }

    public class ServiceUsageDTO
    {
        public string ServiceName { get; set; }
        public int UsageCount { get; set; }
    }

    public class RoomDTo
    {
        public string Name { get; set; }
    }


}
