using NLog;

namespace GameStore.Infrastructure.EmailService
{
    public class EmailService : IEmailService
    {
        public void Send(string address, string header, string body)
        {
            ILogger _logger = LogManager.GetLogger("EmailSendingLogger");

            _logger.Info($"Address: {address}, header: {header}, body: {body}");
        }
    }
}
