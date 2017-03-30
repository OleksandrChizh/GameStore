using GameStore.Services.Interfaces;
using NLog;

namespace GameStore.Infrastructure.Services
{
    public class MobileAppNotificationService : IMobileAppNotificationService
    {
        public void Send(string userId, string message)
        {
            ILogger _logger = LogManager.GetLogger("ManagerMobileMessagesLogger");

            _logger.Info($"UserId: {userId}, Message: {message}");
        }
    }
}
