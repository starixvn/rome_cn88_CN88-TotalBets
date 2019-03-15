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
        private const string ViewPath = "~/Features/WagerAccount/Views/Index.cshtml";
        private const string EventView = "~/Features/WagerAccount/Views/Event.cshtml";

        public WagerAccountController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(WagerAccountQuery message)
            => await ViewAsync(message, ViewPath);

        public async Task<ActionResult> GetByEvent(WagerAccountByEventQuery message)
            => await ViewAsync(message, EventView);
    }
}