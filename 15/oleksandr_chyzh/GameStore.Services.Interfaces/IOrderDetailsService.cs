using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IOrderDetailsService : IDomainEntityService<OrderDetailsDto, int>
    {
        int Create(
            int productId,
            [RangeDecimal] decimal price,
            [RangeShort] short quantity, 
            [RangeFloat] float discount);
    }
}
