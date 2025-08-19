using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IPaymentsService : ICRUDService<Model.Payment, PaymentsSearchObject, PaymentsUpsertRequest, PaymentsUpsertRequest>
    {
        Task<Model.Payment> CreateIntentAsync(PaymentsCreateIntentRequest request);
        Task<Model.Payment> UpdateStatusAsync(PaymentsStatusUpdateRequest request);
        List<Model.Payment> GetByReservation(int reservationId);
        Task<Model.Payment> CreateStripePaymentIntentAsync(int reservationId, decimal amount, string currency = "usd");
        Task UpdateStripePaymentStatusAsync(string paymentIntentId);
    }

}
