using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Authorization;
using GameStore.WebUI.EmailService;
using GameStore.WebUI.Exceptions;
using GameStore.WebUI.Models;
using GameStore.WebUI.PaymentService;
using GameStore.WebUI.PaymentStrategy;
using GameStore.WebUI.PaymentStrategy.ResultBuilders.Interfaces;
using GameStore.WebUI.SmsService;
using GameStore.WebUI.ViewModels.Basket;
using Resources;

namespace GameStore.WebUI.Controllers
{
    [ErrorLogger]
    [PerfomanceCalculator]
    [MvcAuthorise(Roles = "User")]
    public class BasketController : BaseController
    {
        private const string Basket = "BASKET";
        private const string VisaPaymentViewModelKey = "VisaPaymentViewModelKey";

        private readonly IOrderService _orderService;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IIBoxPaymentResultBuilder _iBoxPaymentResultBuilder;
        private readonly IGameService _gameService;
        private readonly IPaymentService _paymentService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public BasketController(
            IOrderService orderService,
            IOrderDetailsService orderDetailsService,
            IIBoxPaymentResultBuilder iBoxPaymentResultBuilder,
            IGameService gameService,
            IPaymentService paymentService,
            IEmailService emailService,
            ISmsService smsService)
        {
            _orderService = orderService;
            _orderDetailsService = orderDetailsService;
            _iBoxPaymentResultBuilder = iBoxPaymentResultBuilder;
            _gameService = gameService;
            _paymentService = paymentService;
            _emailService = emailService;
            _smsService = smsService;
        }

        [HttpGet]
        [ActionName("Buy")]
        public ActionResult AddGameToBasket(string gameKey)
        {
            GameDto game = _gameService.GetGameByKey(gameKey);
            if (game.Deleted)
            {
                throw new GameDeletedException();
            }

            var basket = Session[Basket] as List<Purchase>;
            if (basket == null)
            {
                basket = new List<Purchase>();
                Session[Basket] = basket;
            }

            const short gamesQuantity = 1;
            if (basket.Count(p => p.GameId == game.Id) == 0)
            {
                var purchase = new Purchase(game.Id, game.LanguagesNames["ru"], game.Price, gamesQuantity);
                basket.Add(purchase);
            }
            else
            {
                basket.Single(p => p.GameId == game.Id).Quantity += gamesQuantity;
            }

            CreateOrUpdateCurrentOrder(basket);

            return RedirectToAction("GetCurrentOrderDetails");
        }

        [HttpPost]       
        public HttpStatusCodeResult ChangeGameQuantity(ChangeGameQuantityViewModel model)
        {
            var basket = Session[Basket] as List<Purchase>;
            if (basket == null)
            {
                basket = new List<Purchase>();
                GameDto game = _gameService.Get(model.Id);
                basket.Add(new Purchase(game.Id, game.LanguagesNames["ru"], game.Price, model.Quantity));

                Session[Basket] = basket;
            }
            else
            {
                Purchase purchase = basket.Single(p => p.GameId == model.Id);
                if (model.Quantity == 0)
                {
                    basket.Remove(purchase);
                }
                else
                {
                    purchase.Quantity = model.Quantity;
                }
            }

            CreateOrUpdateCurrentOrder(basket);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ViewResult GetCurrentOrderDetails()
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

            var purchases = Session[Basket] as List<Purchase>;
            if (purchases == null)
            {
                OrderDto order = _orderService.GetCurrentOrderForCustomer((User as IUserPrincipal).Id);
                purchases = order == null ? new List<Purchase>() : CreateBasketFromOrder(order, currentCulture);
                Session[Basket] = purchases;
            }

            for (int i = 0; i < purchases.Count; i++)
            {
                GameDto game = _gameService.Get(purchases[i].GameId);
                purchases[i].GameName = game.LanguagesNames.ContainsKey(currentCulture)
                                                            ? game.LanguagesNames[currentCulture]
                                                            : game.LanguagesNames["ru"];
            }

            return View(purchases);
        }

        [HttpGet]
        public ViewResult MakeOrder()
        {
            var basket = GetBasket();

            IDictionary<int, short> gamesQuantity = basket.ToDictionary(purchase => purchase.GameId, purchase => purchase.Quantity);
            IDictionary<string, short> deficitInformation = _gameService.GetInformationAboutGamesDeficit(gamesQuantity);

            return deficitInformation.Count == 0 ? View(basket) : View("GamesDeficit", deficitInformation);
        }

        [HttpGet]
        public ActionResult SaveOrder(PaymentType type)
        {
            var basket = GetBasket();
            int orderId = CreateOrUpdateCurrentOrder(basket);
            string customerId = (User as IUserPrincipal).Id;
            decimal sum = basket.Sum(p => p.Price * p.Quantity);

            IPaymentStrategy strategy;
            switch (type)
            {
                case PaymentType.Bank:
                    _orderService.Pay(orderId);
                    Session[Basket] = null;
                    List<string> paragraphs = basket.Select(purchase => $"Game name: {purchase.GameName}\n" + $"Quantity: {purchase.Quantity}\n" + $"Price: {purchase.Price}\n\n").ToList();
                    paragraphs.Add($"Total price: {sum}");
                    strategy = new BankPayment(paragraphs.ToArray());
                    break;

                case PaymentType.IBox:
                    _orderService.Pay(orderId);
                    Session[Basket] = null;
                    strategy = new IBoxPayment(customerId, orderId, sum, _iBoxPaymentResultBuilder);
                    break;

                case PaymentType.Visa:
                    string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);
                    strategy = new VisaPayment($"/{currentCulture}/Basket/Visa");
                    break;

                default:
                {
                    throw new UndefinedPaymentTypeException();
                }
            }

            return strategy.GetActionResult();
        }

        [HttpGet]
        public ViewResult Visa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakeRequestToBank(VisaPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Visa", model);
            }

            // throws exception, when doesn't exists
            GetBasket();

            if (!_paymentService.IsUserExists(model.CardNumber, model.CartHoldersName))
            {
                ModelState.AddModelError(string.Empty, Resource.AccountNotFound);
                return View("Visa", model);
            }

            const string code = "qazx";
            string sendingMessage = $"Code: {code}";
            if (model.ConfirmationType == ConfirmationType.UsingPhoneNumber)
            {
                _smsService.Send(model.PhoneNumber, sendingMessage);
            }

            if (model.ConfirmationType == ConfirmationType.UsingEmail)
            {
                _emailService.Send(model.Email, "Code", sendingMessage);
            }

            TempData[VisaPaymentViewModelKey] = model;
            return View("Confirmation");
        }

        [HttpPost]
        public ActionResult ConfirmPayment(ConfirmPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Confirmation", model);
            }

            var data = TempData[VisaPaymentViewModelKey] as VisaPaymentViewModel;
            List<Purchase> basket = GetBasket();
            decimal amountOfPayment = basket.Sum(b => b.Price * b.Quantity);

            PaymentStatus result = PayOrderUsingPaymentService(data, basket, amountOfPayment);

            if (result != PaymentStatus.SuccessfulPayment)
            {
                var message = string.Empty;

                if (result == PaymentStatus.CardDoesNotExist)
                {
                    message = Resource.CardNotFound;
                }

                if (result == PaymentStatus.NotEnoughMoney)
                {
                    message = Resource.NotEnoughMoney;
                }

                if (result == PaymentStatus.PaymentFailed)
                {
                    message = Resource.PaymentFailed;
                }

                return RedirectToAction("ErrorMessage", "Error", new { message });
            }

            const string recipientEmailAddress = "recipient@gamestore.com";
            _emailService.Send(recipientEmailAddress, "new purchase", $"Amount of {amountOfPayment}");

            int orderId = CreateOrUpdateCurrentOrder(basket);
            _orderService.Pay(orderId);
            Session[Basket] = null;

            return View("OrderPaid");
        }

        private List<Purchase> CreateBasketFromOrder(OrderDto order, string currentCulture)
        {
            var purchases = new List<Purchase>();

            foreach (var orderDetails in order.OrderDetails)
            {
                List<GameDto> games = _gameService.GetGamesByIds(order.OrderDetails
                                                            .Select(od => od.ProductId)
                                                            .ToList())
                                                  .ToList();

                GameDto game = games.Single(g => g.Id == orderDetails.ProductId);
                string gameName = game.LanguagesNames.ContainsKey(currentCulture) ? game.LanguagesNames[currentCulture] : game.LanguagesNames["ru"];
                purchases.Add(new Purchase(orderDetails.ProductId, gameName, orderDetails.Price, orderDetails.Quantity));
            }

            return purchases;
        }

        private PaymentStatus PayOrderUsingPaymentService(VisaPaymentViewModel data, List<Purchase> basket, decimal amountOfPayment)
        {
            string[] expirationDateData = data.ExpireDate.Split('/');

            List<GameDto> games = _gameService
                .GetGamesByIds(basket.Select(p => p.GameId).ToList())
                .ToList();

            var purpose = new StringBuilder();
            games.ForEach(g => purpose.Append($"{g.Key}, "));
            purpose.Remove(purpose.Length - 2, 2);

            PaymentStatus result = _paymentService.PayUsingVisa(
                data.CardNumber,
                data.CartHoldersName,
                data.CVV2,
                int.Parse(expirationDateData[0]),
                int.Parse(expirationDateData[1]),
                purpose.ToString(),
                amountOfPayment,
                data.Email,
                data.PhoneNumber);

            return result;
        }

        private List<Purchase> GetBasket()
        {
            var basket = Session[Basket] as List<Purchase>;
            if (basket == null || basket.Count == 0)
            {
                throw new BasketEmptyException();
            }

            return basket;
        }

        private int CreateOrUpdateCurrentOrder(List<Purchase> basket)
        {
            int orderId;
            string customerId = (User as IUserPrincipal).Id;
            OrderDto order = _orderService.GetCurrentOrderForCustomer(customerId);
            if (order == null)
            {
                const int discount = 0;
                List<int> orderDetailsIds = basket.Select(b => _orderDetailsService.Create(b.GameId, b.Price, b.Quantity, discount)).ToList();
                orderId = _orderService.Create(customerId, orderDetailsIds);
            }
            else
            {
                Dictionary<int, short> pairs = basket.ToDictionary(purchase => purchase.GameId, purchase => purchase.Quantity);
                _orderService.Update(order.Id, pairs, customerId);
                orderId = order.Id;
            }

            return orderId;
        }
    }
}