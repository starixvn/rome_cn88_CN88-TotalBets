namespace CN88.TotalBets.Site.Features.Overview
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Shared.Controllers;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class OverViewController : SiteBaseController
    {
        private const string ViewPath = "~/Features/Overview/Views/Index.cshtml";

        public OverViewController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(OverViewQuery message)
            => await ViewAsync(message, ViewPath);
    }
}