using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IMobileAppNotificationService
    {
        void Send(
            [NonEmptyString] string userId,
            [NonEmptyString] string message);
    }
}
