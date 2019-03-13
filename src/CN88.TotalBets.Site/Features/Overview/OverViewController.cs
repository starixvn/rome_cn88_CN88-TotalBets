namespace CN88.TotalBets.Site.Features.Overview
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Shared.Controllers;
    using TotalBets.Shared.Services;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class OverViewController : SiteBaseController
    {
        private const string ViewPath = "~/Features/Overview/Views/Index.cshtml";

        private readonly CacheRefService cacheRefService;

        public OverViewController(IMediator mediator, CacheRefService cacheRefService)
            : base(mediator)
        {
            this.cacheRefService = cacheRefService;
        }

        public async Task<ActionResult> Index(OverViewQuery message)
        {
            message.GetSportDisplayName = cacheRefService.GetSportName;
            return await ViewAsync(message, ViewPath);
        }
    }
}