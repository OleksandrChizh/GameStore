using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface ISmsService
    {
        void Send(
            [NonEmptyString] string phoneNumber,
            [NonEmptyString] string message);
    }
}
