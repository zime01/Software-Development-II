using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IReservationsService : ICRUDService<Model.Reservation, ReservationsSearchObject, ReservationsUpsertRequest, ReservationsUpsertRequest>
    {
        List<Model.Reservation> GetByUserId(int id);
        Task<Model.Reservation> UpdateStatusAsync(int reservationId, string newStatus, int actingUserId);
        Task<Model.Reservation> InsertAsync(ReservationsUpsertRequest request);
        List<Model.Reservation> GetReservationsByHotel(int hotelId);
    }
}
