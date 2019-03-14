namespace CN88.TotalBets.Site.Features.InPlay
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Shared.Controllers;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class InPlayController : SiteBaseController
    {
        private const string ViewPath = "~/Features/InPlay/Views/Index.cshtml";

        public InPlayController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(InPlayQuery message)
            => await ViewAsync(message, ViewPath);
    }
}