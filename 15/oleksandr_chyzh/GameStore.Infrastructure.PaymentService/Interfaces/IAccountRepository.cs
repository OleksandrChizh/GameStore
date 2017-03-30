using GameStore.Infrastructure.PaymentService.Models;

namespace GameStore.Infrastructure.PaymentService.Interfaces
{
    public interface IAccountRepository
    {
        void Create(Account account);

        void Update(Account account);

        Account Get(string cardNumber);

        void Clear();
    }
}
