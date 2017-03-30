using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Infrastructure.Services
{
    public class OrderPaidNotificationCenter : IOrderPaidNotificationCenter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IMobileAppNotificationService _notificationService;

        public OrderPaidNotificationCenter(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ISmsService smsService,
            IMobileAppNotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _smsService = smsService;
            _notificationService = notificationService;
        }

        public void NotifyManagers(int orderId)
        {
            string message = CreateMessage(orderId);
            IEnumerable<User> managers = _unitOfWork.Users.Find(u => u.Roles.Any(r => r.Name == "Manager"));

            foreach (var manager in managers)
            {
                switch (manager.NotificationType)
                {
                    case NotificationType.Email:
                        _emailService.Send(manager.UserId, "Order paid", message);
                        break;
                    case NotificationType.Sms:
                        _smsService.Send(manager.UserId, message);
                        break;
                    case NotificationType.MobileApp:
                        _notificationService.Send(manager.UserId, message);
                        break;
                }
            }
        }

        private string CreateMessage(int orderId)
        {
            Order order = GetValidOrder(orderId);
            List<int> gamesIds = order.OrderDetails.Select(od => od.ProductId).ToList();
            List<Game> games = _unitOfWork.Games.Find(g => gamesIds.Contains(g.Id)).ToList();

            string message = "Order paid. Details: ";
            foreach (var details in order.OrderDetails)
            {
                Game game = games.Single(g => g.Id == details.ProductId);
                message += $" [Game: {game.Name}, quantity: {details.Quantity}] ";
            }

            return message;
        }

        private Order GetValidOrder(int orderId)
        {
            Order order = _unitOfWork.Orders.Get(orderId);
            if (order == null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            if (order.PayingDate == default(DateTime))
            {
                throw new OrderNotPaidException();
            }

            return order;
        }
    }
}