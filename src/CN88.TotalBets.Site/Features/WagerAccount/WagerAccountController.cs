namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Shared.Controllers;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class WagerAccountController : SiteBaseController
    {
        private const string OurightViewPath = "~/Features/WagerAccount/Outright/Views/Index.cshtml";
        private const string EventViewPath = "~/Features/WagerAccount/Event/Views/Index.cshtml";

        public WagerAccountController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> GetByOutright(WagerAccountByOutrightQuery message)
            => await ViewAsync(message, OurightViewPath);

        public async Task<ActionResult> GetByEvent(WagerAccountByEventQuery message)
            => await ViewAsync(message, EventViewPath);
    }
}