using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.DataAccess.Filters;
using GameStore.Infrastructure.DataAccess.Implementations;
using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.Infrastructure.Services;
using GameStore.Services.Interfaces;
using GameStore.WebUI.EmailService;
using GameStore.WebUI.PaymentService;
using GameStore.WebUI.PaymentStrategy.ResultBuilders.Implementations;
using GameStore.WebUI.PaymentStrategy.ResultBuilders.Interfaces;
using GameStore.WebUI.SmsService;
using GameStore.WebUI.Utils;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace GameStore.WebUI
{
    public class DependenciesConfiguration
    {
        public static IKernel RegisterDependencies(StandardKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<ICommentService>().To<CommentService>().Intercept().With<ValidationInterceptor>();
            kernel.Bind<IGameService>().To<GameService>().Intercept().With<ValidationInterceptor>();
            kernel.Bind<IGenreService>().To<GenreService>().Intercept().With<ValidationInterceptor>();
            kernel.Bind<IPlatformTypeService>().To<PlatformTypeService>().Intercept().With<ValidationInterceptor>();
            kernel.Bind<IPublisherService>().To<PublisherService>().Intercept().With<ValidationInterceptor>();
            kernel.Bind<IOrderDetailsService>().To<OrderDetailsService>().Intercept().With<ValidationInterceptor>();
            kernel.Bind<IOrderService>().To<OrderService>().Intercept().With<ValidationInterceptor>();
            kernel.Bind<IUserService>().To<UserService>().Intercept().With<ValidationInterceptor>();
            kernel.Bind<IRoleService>().To<RoleService>().Intercept().With<ValidationInterceptor>();

            kernel.Bind<IIBoxPaymentResultBuilder>().To<IBoxPaymentHtmlResultBuilder>().Intercept().With<ValidationInterceptor>();

            kernel.Bind<IFilter<PlatformType>>().To<PlatformTypesForIdsFilter>().Named(nameof(PlatformTypesForIdsFilter));
            kernel.Bind<IFilter<Genre>>().To<GenresForIdsFilter>().Named(nameof(GenresForIdsFilter));
            kernel.Bind<IFilter<Publisher>>().To<PublishersForIdsFilter>().Named(nameof(PublishersForIdsFilter));
            kernel.Bind<IFilter<OrderDetails>>().To<OrderDetailsForIdsFilter>().Named(nameof(OrderDetailsForIdsFilter));
            kernel.Bind<IFilter<Game>>().To<GamesByGameNameFilter>().Named(nameof(GamesByGameNameFilter));
            kernel.Bind<IFilter<Game>>().To<GamesByGenresWithIdsFilter>().Named(nameof(GamesByGenresWithIdsFilter));
            kernel.Bind<IFilter<Game>>().To<GamesByPlatformTypesWithIdsFilter>().Named(nameof(GamesByPlatformTypesWithIdsFilter));
            kernel.Bind<IFilter<Game>>().To<GamesByPriceRangeFilter>().Named(nameof(GamesByPriceRangeFilter));
            kernel.Bind<IFilter<Game>>().To<GamesByPublishersWithIdsFilter>().Named(nameof(GamesByPublishersWithIdsFilter));
            kernel.Bind<IFilter<Game>>().To<GamesByPublishingDateFilter>().Named(nameof(GamesByPublishingDateFilter));
            kernel.Bind<IFilter<Game>>().To<GamesBySortingObjectFilter>().Named(nameof(GamesBySortingObjectFilter));
            kernel.Bind<IFilter<Game>>().To<GamesPaginationFilter>().Named(nameof(GamesPaginationFilter));
            kernel.Bind<IFilter<Game>>().To<SortedGamesFilter>().Named(nameof(SortedGamesFilter));
            kernel.Bind<IFilter<Order>>().To<OrdersByDateRangeFilter>().Named(nameof(OrdersByDateRangeFilter));

            kernel.Bind<IFilterFactory>().ToFactory(() => new FilterProvider());

            kernel.Bind<IPipeline<Game>>().To<Pipeline<Game>>();
            kernel.Bind<IPipeline<Order>>().To<Pipeline<Order>>();

            kernel.Bind<IDatabaseSynchronizer>().To<DatabaseSynchronizer>();

            kernel.Bind<ISmsService>().To<SmsServiceClient>();
            kernel.Bind<IEmailService>().To<EmailServiceClient>();
            kernel.Bind<IPaymentService>().To<PaymentServiceClient>();

            return kernel;
        }
    }
}