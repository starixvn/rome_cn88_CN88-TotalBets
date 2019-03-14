namespace CN88.TotalBets.Site.App_Start
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Web.Mvc;
    using Fanex.Data.Repository;
    using Fanex.RemotingRef;
    using MediatR;
    using Shared.Controllers;
    using Shared.Models;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Sunplus.Agent.Model.Base;
    using Sunplus.Agent.Model.Entities;
    using TotalBets.Features.Events;
    using TotalBets.Features.Leagues;
    using TotalBets.Features.Outright;
    using TotalBets.Features.Overview;
    using TotalBets.Features.SportTypeGroup;
    using TotalBets.Features.Summay;
    using TotalBets.Shared.Models;
    using TotalBets.Shared.Services;

    public static class DependencyConfig
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Reviewed")]
        public static void Init()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            RegisterMediator(container);

            RegisterDataAccesses(container);

            RegisterServices(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void RegisterMediator(Container container)
        {
            var assemblies = GetAssemblies();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);

            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(ICancellableAsyncNotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IPipelineBehavior<,>), assemblies);

            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(SiteBaseController).Assembly;
        }

        private static void RegisterDataAccesses(Container container)
        {
            container.Register<IDynamicRepository, DynamicRepository>(Lifestyle.Singleton);
            container.RegisterSingleton<SportTypeGroupDataAccess, SportTypeGroupDataAccessImpl>();
        }

        private static void RegisterServices(Container container)
        {
            container.RegisterSingleton<IUserSession, UserSessionExt>();
            container.RegisterSingleton<IUser, User>();
            container.RegisterSingleton(Fanex.Logging.Logger.Log);
            container.RegisterSingleton<IRemotingRefService, RemotingRefService>();
            container.RegisterSingleton<CacheRefService, CacheRefServiceImpl>();
            container.Register<SportTypeGroupService>(
                () => new SportTypeGroupServiceImpl(
                    container.GetInstance<SportTypeGroupDataAccess>(),
                    container.GetInstance<CacheRefService>().GetSportName),
                Lifestyle.Singleton);

            container.RegisterSingleton<IOverViewService, OverViewService>();
            container.RegisterSingleton<EventService, EventServiceImpl>();
            container.RegisterSingleton<OutrightService, OutrightServiceImpl>();
            container.RegisterSingleton<LeagueService, LeagueServiceImpl>();
            container.RegisterSingleton<SummaryService, SummaryServiceImpl>();

        }
    }
}
