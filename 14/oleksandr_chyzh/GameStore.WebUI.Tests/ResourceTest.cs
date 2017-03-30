using NUnit.Framework;
using Resources;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class ResourceTest
    {
        [Test]
        public void GetStringForAcceptTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Accept);
        }

        [Test]
        public void GetStringForAddingDateTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.AddingDate);
        }

        [Test]
        public void GetStringForAllTimeTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.AllTime);
        }

        [Test]
        public void GetStringForAnswerTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Answer);
        }

        [Test]
        public void GetStringForBanForUserTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.BanForUser);
        }

        [Test]
        public void GetStringForBankTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Bank);
        }

        [Test]
        public void GetStringForBanMessageTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.BanMessage);
        }

        [Test]
        public void GetStringForBannedToTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.BannedTo);
        }

        [Test]
        public void GetStringForBasketTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Basket);
        }

        [Test]
        public void GetStringForBasketIsEmptyTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.BasketIsEmpty);
        }

        [Test]
        public void GetStringForBodyTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Body);
        }

        [Test]
        public void GetStringForBuyTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Buy);
        }

        [Test]
        public void GetStringForByPriceAscTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ByPriceAsc);
        }

        [Test]
        public void GetStringForByPriceDescTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ByPriceDesc);
        }

        [Test]
        public void GetStringForCancelTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Cancel);
        }

        [Test]
        public void GetStringForCardNumberTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.CardNumber);
        }

        [Test]
        public void GetStringForCartHoldersNameTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.CartHoldersName);
        }

        [Test]
        public void GetStringForChooseTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Choose);
        }

        [Test]
        public void GetStringForChooseLangTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ChooseLang);
        }

        [Test]
        public void GetStringForCommentsTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Comments);
        }

        [Test]
        public void GetStringForCommentsForGameTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.CommentsForGame);
        }

        [Test]
        public void GetStringForCommentWasDeletedTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.CommentWasDeleted);
        }

        [Test]
        public void GetStringForCompanyNameTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.CompanyName);
        }

        [Test]
        public void GetStringForConfirmPasswordTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ConfirmPassword);
        }

        [Test]
        public void GetStringForContainEnglishTranslationTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ContainEnglishTranslation);
        }

        [Test]
        public void GetStringForCreateTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Create);
        }

        [Test]
        public void GetStringForCustomerIdentifierTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.CustomerIdentifier);
        }

        [Test]
        public void GetStringForDateFromMustBeEarlierThenDateToTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.DateFromMustBeEarlierThenDateTo);
        }

        [Test]
        public void GetStringForDateRangeTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.DateRange);
        }

        [Test]
        public void GetStringForDefaultTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Default);
        }

        [Test]
        public void GetStringForDeleteTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Delete);
        }

        [Test]
        public void GetStringForDeletingTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Deleting);
        }

        [Test]
        public void GetStringForDeletingGenreTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.DeletingGenre);
        }

        [Test]
        public void GetStringForDeletingPlatformTypeTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.DeletingPlatformType);
        }

        [Test]
        public void GetStringForDeletingPublisherTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.DeletingPublisher);
        }

        [Test]
        public void GetStringForDeletingUserTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.DeletingUser);
        }

        [Test]
        public void GetStringForDeletingUserMessageTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.DeletingUserMessage);
        }

        [Test]
        public void GetStringForDeliverTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Deliver);
        }

        [Test]
        public void GetStringForDescriptionTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Description);
        }

        [Test]
        public void GetStringForDetailsTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Details);
        }

        [Test]
        public void GetStringForDiscountedTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Discounted);
        }

        [Test]
        public void GetStringForDownloadTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Download);
        }

        [Test]
        public void GetStringForEditTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Edit);
        }

        [Test]
        public void GetStringForEmailTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Email);
        }

        [Test]
        public void GetStringForEnglishDescriptionTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.EnglishDescription);
        }

        [Test]
        public void GetStringForEnglishDescriptionIsRequiredTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.EnglishDescriptionIsRequired);
        }

        [Test]
        public void GetStringForEnglishNameIsRequiredTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.EnglishNameIsRequired);
        }

        [Test]
        public void GetStringForEntranceTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Entrance);
        }

        [Test]
        public void GetStringForErrorTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Error);
        }

        [Test]
        public void GetStringForErrorOccuredTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ErrorOccured);
        }

        [Test]
        public void GetStringForExpiryDateTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ExpiryDate);
        }

        [Test]
        public void GetStringForFiftyItemsTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.FiftyItems);
        }

        [Test]
        public void GetStringForFilterTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Filter);
        }

        [Test]
        public void GetStringForForbiddenTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Forbidden);
        }

        [Test]
        public void GetStringForForbiddenMessageTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ForbiddenMessage);
        }

        [Test]
        public void GetStringForFromTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.From);
        }

        [Test]
        public void GetStringForGameCreatingTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.GameCreating);
        }

        [Test]
        public void GetStringForGameMustContainAtLeastOnePlatformTypeTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.GameMustContainAtLeastOnePlatformType);
        }

        [Test]
        public void GetStringForGamesTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Games);
        }

        [Test]
        public void GetStringForGamesNotEnoughForOrderMakingTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.GamesNotEnoughForOrderMaking);
        }

        [Test]
        public void GetStringForGamesOnPageTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.GamesOnPage);
        }

        [Test]
        public void GetStringForGenresTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Genres);
        }

        [Test]
        public void GetStringForHistoryTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.History);
        }

        [Test]
        public void GetStringForHomePageTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.HomePage);
        }

        [Test]
        public void GetStringForIBoxTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.IBox);
        }

        [Test]
        public void GetStringForIdentifierTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Identifier);
        }

        [Test]
        public void GetStringForInvalidEmailTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.InvalidEmail);
        }

        [Test]
        public void GetStringForInvalidPasswordTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.InvalidPassword);
        }

        [Test]
        public void GetStringForKeyTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Key);
        }

        [Test]
        public void GetStringForLastMonthTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.LastMonth);
        }

        [Test]
        public void GetStringForLastWeekTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.LastWeek);
        }

        [Test]
        public void GetStringForLastYearTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.LastYear);
        }

        [Test]
        public void GetStringForLoginTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Login);
        }

        [Test]
        public void GetStringForLogoutTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Logout);
        }

        [Test]
        public void GetStringForMakeOrderTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.MakeOrder);
        }

        [Test]
        public void GetStringForMaxTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Max);
        }

        [Test]
        public void GetStringForMaxCommentBodyLength200Translate()
        {
            Assert.IsInstanceOf<string>(Resource.MaxCommentBodyLength200);
        }

        [Test]
        public void GetStringForMaxCommentNameLength50Translate()
        {
            Assert.IsInstanceOf<string>(Resource.MaxCommentNameLength50);
        }

        [Test]
        public void GetStringForMaxDescriptionLength500Translate()
        {
            Assert.IsInstanceOf<string>(Resource.MaxDescriptionLength500);
        }

        [Test]
        public void GetStringForMaxGameNameLength100Translate()
        {
            Assert.IsInstanceOf<string>(Resource.MaxGameNameLength100);
        }

        [Test]
        public void GetStringForMaxKeyLength30Translate()
        {
            Assert.IsInstanceOf<string>(Resource.MaxKeyLength30);
        }

        [Test]
        public void GetStringForMaxNameLenght70Translate()
        {
            Assert.IsInstanceOf<string>(Resource.MaxNameLenght70);
        }

        [Test]
        public void GetStringForMaxPlatformTypeLength50Translate()
        {
            Assert.IsInstanceOf<string>(Resource.MaxPlatformTypeLength50);
        }

        [Test]
        public void GetStringForMinTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Min);
        }

        [Test]
        public void GetStringForMinGameNameLength3Translate()
        {
            Assert.IsInstanceOf<string>(Resource.MinGameNameLength3);
        }

        [Test]
        public void GetStringForMinPriceMustBeLessThenMaxTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.MinPriceMustBeLessThenMax);
        }

        [Test]
        public void GetStringForMostCommendedTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.MostCommended);
        }

        [Test]
        public void GetStringForMostPopularTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.MostPopular);
        }

        [Test]
        public void GetStringForNameTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Name);
        }

        [Test]
        public void GetStringForNewTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.New);
        }

        [Test]
        public void GetStringForNewCommentTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.NewComment);
        }

        [Test]
        public void GetStringForNoTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.No);
        }

        [Test]
        public void GetStringForNotEqualPasswordsTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.NotEqualPasswords);
        }

        [Test]
        public void GetStringForNotPaidTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.NotPaid);
        }

        [Test]
        public void GetStringForOneHundredItemsTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.OneHundredItems);
        }

        [Test]
        public void GetStringForOrderTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Order);
        }

        [Test]
        public void GetStringForOrderDateTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.OrderDate);
        }

        [Test]
        public void GetStringForOrderPaidTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.OrderPaid);
        }

        [Test]
        public void GetStringForOrdersTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Orders);
        }

        [Test]
        public void GetStringForOrderShippedTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.OrderShipped);
        }

        [Test]
        public void GetStringForOrderStatusTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.OrderStatus);
        }

        [Test]
        public void GetStringForPageNotFoundTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PageNotFound);
        }

        [Test]
        public void GetStringForPaidTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Paid);
        }

        [Test]
        public void GetStringForParentGenreTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ParentGenre);
        }

        [Test]
        public void GetStringForPasswordTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Password);
        }

        [Test]
        public void GetStringForPayTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Pay);
        }

        [Test]
        public void GetStringForPayingDateTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PayingDate);
        }

        [Test]
        public void GetStringForPaymentTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Payment);
        }

        [Test]
        public void GetStringForPaymentResultTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PaymentResult);
        }

        [Test]
        public void GetStringForPayUsingInvoiceFileTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PayUsingInvoiceFile);
        }

        [Test]
        public void GetStringForPayViaBankSiteTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PayViaBankSite);
        }

        [Test]
        public void GetStringForPayViaIBoxTerminalTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PayViaIBoxTerminal);
        }

        [Test]
        public void GetStringForPlatformTypesTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PlatformTypes);
        }

        [Test]
        public void GetStringForPriceTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Price);
        }

        [Test]
        public void GetStringForPriceRangeTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PriceRange);
        }

        [Test]
        public void GetStringForPublishedForTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PublishedFor);
        }

        [Test]
        public void GetStringForPublisherTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Publisher);
        }

        [Test]
        public void GetStringForPublisherCreatingTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PublisherCreating);
        }

        [Test]
        public void GetStringForPublishersTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Publishers);
        }

        [Test]
        public void GetStringForPublishingDateTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.PublishingDate);
        }

        [Test]
        public void GetStringForQuantityTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Quantity);
        }

        [Test]
        public void GetStringForQuoteTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Quote);
        }

        [Test]
        public void GetStringForRegisterTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Register);
        }

        [Test]
        public void GetStringForRegistrationTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Registration);
        }

        [Test]
        public void GetStringForRemoveTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Remove);
        }

        [Test]
        public void GetStringForRepliedToTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.RepliedTo);
        }

        [Test]
        public void GetStringForRolesTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Roles);
        }

        [Test]
        public void GetStringForSaveOrderTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.SaveOrder);
        }

        [Test]
        public void GetStringForSearchByNameTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.SearchByName);
        }

        [Test]
        public void GetStringForSendTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Send);
        }

        [Test]
        public void GetStringForShippedDateTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ShippedDate);
        }

        [Test]
        public void GetStringForSortingTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Sorting);
        }

        [Test]
        public void GetStringForSubmitTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Submit);
        }

        [Test]
        public void GetStringForTenItemsTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.TenItems);
        }

        [Test]
        public void GetStringForThreeYearsTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ThreeYears);
        }

        [Test]
        public void GetStringForToTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.To);
        }

        [Test]
        public void GetStringForToBasketTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ToBasket);
        }

        [Test]
        public void GetStringForToHomePageTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ToHomePage);
        }

        [Test]
        public void GetStringForTotalAmountOfGameForSiteTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.TotalAmountOfGameForSite);
        }

        [Test]
        public void GetStringForTotalPriceTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.TotalPrice);
        }

        [Test]
        public void GetStringForTwentyItemsTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.TwentyItems);
        }

        [Test]
        public void GetStringForTwoYearsTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.TwoYears);
        }

        [Test]
        public void GetStringForUnitsInStockTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.UnitsInStock);
        }

        [Test]
        public void GetStringForUnknownTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Unknown);
        }

        [Test]
        public void GetStringForUpdateTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Update);
        }

        [Test]
        public void GetStringForUpdatingTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Updating);
        }

        [Test]
        public void GetStringForUpdatingUserTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.UpdatingUser);
        }

        [Test]
        public void GetStringForUsersTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Users);
        }

        [Test]
        public void GetStringForViewsCountTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.ViewsCount);
        }

        [Test]
        public void GetStringForVisaTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Visa);
        }

        [Test]
        public void GetStringForYesTranslate()
        {
            Assert.IsInstanceOf<string>(Resource.Yes);
        }
    }
}
