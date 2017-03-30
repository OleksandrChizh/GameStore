using System.ServiceModel;

namespace GameStore.Infrastructure.EmailService
{
    [ServiceContract]
    public interface IEmailService
    {
        [OperationContract]
        void Send(string address, string header, string body);
    }
}
