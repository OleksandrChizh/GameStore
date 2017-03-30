using System.Runtime.Serialization;

namespace GameStore.Infrastructure.PaymentService.Models
{
    [DataContract]
    public class Account
    {
        public Account(string cardNumber, User owner, decimal amountOfMoney)
        {
            CardNumber = cardNumber;
            Owner = owner;
            AmountOfMoney = amountOfMoney;
        }

        public Account()
        {
        }

        [DataMember]
        public string CardNumber { get; set; }

        [DataMember]
        public User Owner { get; set; }

        [DataMember]
        public decimal AmountOfMoney { get; set; }

        public override bool Equals(object obj)
        {
            return CardNumber == ((Account)obj).CardNumber;
        }

        public override int GetHashCode()
        {
            return CardNumber.GetHashCode();
        }
    }
}