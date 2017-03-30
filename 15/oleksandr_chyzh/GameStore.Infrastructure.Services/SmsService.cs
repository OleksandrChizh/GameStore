using GameStore.Services.Interfaces;
using NLog;

namespace GameStore.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        public void Send(string phoneNumber, string message)
        {
            ILogger _logger = LogManager.GetLogger("ManagerSmsMessagesLogger");

            _logger.Info($"PhoneNumber: {phoneNumber}, Message: {message}");
        }
    }
}
