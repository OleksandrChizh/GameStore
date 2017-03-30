using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using GameStore.Infrastructure.PaymentService.Exceptions.ClientExceptions;
using GameStore.Infrastructure.PaymentService.Exceptions.ServerExceptions;
using GameStore.Infrastructure.PaymentService.Interfaces;
using GameStore.Infrastructure.PaymentService.Models;

namespace GameStore.Infrastructure.PaymentService.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        public void Create(Account account)
        {
            List<Account> accounts = GetAccounts();

            if (accounts.Any(a => a.CardNumber == account.CardNumber))
            {
                throw new AccountCreatedException();
            }

            accounts.Add(account);

            SetAccounts(accounts);
        }

        public void Update(Account account)
        {
            List<Account> accounts = GetAccounts();

            int index = accounts.IndexOf(account);
            if (index == -1)
            {
                throw new FaultException<AccountUpdateFault>(new AccountUpdateFault());
            }

            accounts[index].AmountOfMoney = account.AmountOfMoney;

            SetAccounts(accounts);
        }

        public Account Get(string cardNumber)
        {
            return GetAccounts().FirstOrDefault(a => a.CardNumber == cardNumber);
        }

        public void Clear()
        {
            SetAccounts(new List<Account>());
        }

        private List<Account> GetAccounts()
        {
            Account[] accounts;

            var jsonFormatter = new DataContractJsonSerializer(typeof(Account[]));

            using (var fs = new FileStream(@"D:\accounts.json", FileMode.OpenOrCreate))
            {
                try
                {
                    accounts = (Account[])jsonFormatter.ReadObject(fs);
                }
                catch (SerializationException)
                {
                    return new List<Account>();
                }
            }

            return accounts?.ToList() ?? new List<Account>();
        }

        private void SetAccounts(List<Account> accounts)
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(Account[]));

            using (var fs = new FileStream(@"D:\accounts.json", FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, accounts.ToArray());
            }
        }
    }
}