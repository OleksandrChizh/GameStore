using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Dto.Utils;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Infrastructure.Services
{
    public class OrderDetailsService : Service, IOrderDetailsService
    {
        public OrderDetailsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public OrderDetailsDto Get(int id)
        {
            OrderDetails orderDetails = UnitOfWork.OrderDetails.Get(id);
            if (orderDetails == null)
            {
                throw new EntityNotFoundException(typeof(OrderDetails));
            }

            return orderDetails.ToDto();
        }

        public int Create(int productId, decimal price, short quantity, float discount)
        {
            Game game = UnitOfWork.Games.Get(productId);
            if (game == null)
            {
                throw new EntityNotFoundException(typeof(Game));
            }

            if (game.UnitsInStock < quantity)
            {
                throw new GameDeficitException();
            }

            game.UnitsInStock -= quantity;
            var orderDetails = new OrderDetails(productId, price, quantity, discount);

            UnitOfWork.Games.Update(game);
            UnitOfWork.OrderDetails.Create(orderDetails);
            UnitOfWork.Save();
            return orderDetails.Id;
        }
    }
}
