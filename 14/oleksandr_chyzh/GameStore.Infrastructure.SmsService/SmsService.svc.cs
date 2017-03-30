using NLog;

namespace GameStore.Infrastructure.SmsService
{
    public class SmsService : ISmsService
    {
        public void Send(string phoneNumber, string message)
        {
            ILogger _logger = LogManager.GetLogger("SmsSendingLogger");

            _logger.Info($"PhoneNumber: {phoneNumber}, message: {message}");
        }
    }
}
