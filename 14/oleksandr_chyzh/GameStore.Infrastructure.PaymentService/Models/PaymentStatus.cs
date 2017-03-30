using System.Runtime.Serialization;

namespace GameStore.Infrastructure.PaymentService.Models
{
    [DataContract]
    public enum PaymentStatus
    {
        [EnumMember]
        SuccessfulPayment,

        [EnumMember]
        NotEnoughMoney,

        [EnumMember]
        CardDoesNotExist,

        [EnumMember]
        PaymentFailed
    }
}