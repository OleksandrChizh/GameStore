using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(
            [NonEmptyString] string emailTo,
            [NonEmptyString] string subject,
            [NonEmptyString] string body);
    }
}
