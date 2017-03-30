using System.Runtime.Serialization;

namespace GameStore.Infrastructure.PaymentService.Models
{
    [DataContract]
    public class User
    {
        public User(string name, string surname, string accountNumber, string email)
        {
            Name = name;
            Surname = surname;
            AccountNumber = accountNumber;
            Email = email;
        }

        public User()
        {
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string AccountNumber { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}