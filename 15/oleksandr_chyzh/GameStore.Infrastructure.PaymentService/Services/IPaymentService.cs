using System.ServiceModel;
using GameStore.Infrastructure.PaymentService.Models;

namespace GameStore.Infrastructure.PaymentService.Services
{
    [ServiceContract]
    public interface IPaymentService
    {
        [OperationContract]
        PaymentStatus PayUsingVisa(
            string cardNumber,
            string fullName,
            string cvvCode,
            int expirationMonth,
            int expirationYear,
            string purpose,
            decimal amountOfPayment,
            string email = null,
            string phoneNumber = null);

        [OperationContract]
        PaymentStatus PayUsingMasterCard(
            string cardNumber,
            string fullName,
            string cvvCode,
            int expirationMonth,
            int expirationYear,
            string purpose,
            decimal amountOfPayment,
            string email = null,
            string phoneNumber = null);

        [OperationContract]
        bool IsUserExists(string cardNumber, string fullName);
    }
}
