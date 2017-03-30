using System.ServiceModel;

namespace GameStore.Infrastructure.SmsService
{
    [ServiceContract]
    public interface ISmsService
    {
        [OperationContract]
        void Send(string phoneNumber, string message);
    }
}
