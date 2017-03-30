using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using GameStore.Infrastructure.PaymentService.Exceptions.ClientExceptions;
using GameStore.Infrastructure.PaymentService.Implementations;
using GameStore.Infrastructure.PaymentService.Interfaces;
using GameStore.Infrastructure.PaymentService.Models;
using GameStore.Infrastructure.PaymentService.Services;
using NLog;

namespace GameStore.Infrastructure.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountRepository _accountRepository;

        public PaymentService()
        {
            _accountRepository = new AccountRepository();

            SetTestData();
        }

        public PaymentStatus PayUsingVisa(
            string cardNumber,
            string fullName,
            string cvvCode,
            int expirationMonth,
            int expirationYear,
            string purpose,
            decimal amountOfPayment,
            string email = null,
            string phoneNumber = null)
        {
            try
            {
                return Pay(new TransferInformation
                {
                    CardNumber = cardNumber,
                    FullName = fullName,
                    CvvCode = cvvCode,
                    ExpirationMonth = expirationMonth,
                    ExpirationYear = expirationYear,
                    Purpose = purpose,
                    AmountOfPayment = amountOfPayment,
                    Email = email,
                    PhoneNumber = phoneNumber
                });
            }
            catch (Exception)
            {
                return PaymentStatus.PaymentFailed;
            }
        }

        public PaymentStatus PayUsingMasterCard(
            string cardNumber,
            string fullName,
            string cvvCode,
            int expirationMonth,
            int expirationYear,
            string purpose,
            decimal amountOfPayment,
            string email = null,
            string phoneNumber = null)
        {
            try
            {
                return Pay(new TransferInformation
                {
                    CardNumber = cardNumber,
                    FullName = fullName,
                    CvvCode = cvvCode,
                    ExpirationMonth = expirationMonth,
                    ExpirationYear = expirationYear,
                    Purpose = purpose,
                    AmountOfPayment = amountOfPayment,
                    Email = email,
                    PhoneNumber = phoneNumber
                });
            }
            catch (Exception)
            {
                return PaymentStatus.PaymentFailed;
            }
        }

        public bool IsUserExists(string cardNumber, string fullName)
        {
            string[] names = fullName.Split(' ');
            Account account = _accountRepository.Get(cardNumber);

            return account != null && account.Owner.Name == names[0] && account.Owner.Surname == names[1];
        }

        private PaymentStatus Pay(TransferInformation data)
        {
            if (!IsArgumentsCorrect(data))
            {
                return PaymentStatus.PaymentFailed;
            }

            string[] names = data.FullName.Split(' ');
            Account account = _accountRepository.Get(data.CardNumber);
            if (account == null || account.Owner.Name != names[0] || account.Owner.Surname != names[1])
            {
                return PaymentStatus.CardDoesNotExist;
            }

            if (account.AmountOfMoney < data.AmountOfPayment)
            {
                return PaymentStatus.NotEnoughMoney;
            }

            account.AmountOfMoney -= data.AmountOfPayment;
            _accountRepository.Update(account);

            LogTransfer(data);

            return PaymentStatus.SuccessfulPayment;
        }

        private void LogTransfer(TransferInformation data)
        {
            ILogger _logger = LogManager.GetLogger("TransfersLogger");

            _logger.Info($"Card number: {data.CardNumber}, full name: {data.FullName}, purpose: {data.Purpose}, amount of payment: {data.AmountOfPayment}");
        }

        private bool IsArgumentsCorrect(TransferInformation data)
        {         
            if (data.ExpirationYear < 100 && data.ExpirationYear > 0)
            {
                data.ExpirationYear += 2000;
            }

            return Validator.TryValidateObject(data, new ValidationContext(data), new List<ValidationResult>(), true);
        }

        private void SetTestData()
        {
            try
            {
                _accountRepository.Clear();

                const string cardNumber = "1234567812345678";
                var user = new User("Name", "Surname", cardNumber, "user@mail.com");
                var account = new Account(cardNumber, user, 100);

                const string cardNumber1 = "8765432112345678";
                var user1 = new User("Name", "Surname", cardNumber1, "user@mail.com");
                var account1 = new Account(cardNumber1, user1, 100);

                _accountRepository.Create(account);
                _accountRepository.Create(account1);
            }
            catch (Exception)
            {
                throw new FaultException<ServerFault>(new ServerFault());
            }
        }
    }
}
