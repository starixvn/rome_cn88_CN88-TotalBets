namespace CN88.TotalBets.Site.Features.Started
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Shared.Controllers;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class StartedController : SiteBaseController
    {
        private const string ViewPath = "~/Features/Started/Views/Index.cshtml";

        public StartedController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(StartedQuery message)
            => await ViewAsync(message, ViewPath);
    }
}