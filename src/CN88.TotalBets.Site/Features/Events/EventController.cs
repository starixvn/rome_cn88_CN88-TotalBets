namespace CN88.TotalBets.Site.Features.Events
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Shared.Controllers;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class EventController : SiteBaseController
    {
        private const string ViewPath = "~/Features/Events/Views/{0}.cshtml";

        public EventController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(EventQuery message)
            => await ViewAsync(message, string.Format(ViewPath, nameof(Index)));

        public async Task<ActionResult> InPlay(EventQuery message)
            => await ViewAsync(message, string.Format(ViewPath, nameof(InPlay)));

        public async Task<ActionResult> Today(EventQuery message)
            => await ViewAsync(message, string.Format(ViewPath, nameof(Today)));

        public async Task<ActionResult> Early(EventQuery message)
            => await ViewAsync(message, string.Format(ViewPath, nameof(Early)));

        public async Task<ActionResult> Started(EventQuery message)
            => await ViewAsync(message, string.Format(ViewPath, nameof(Started)));
    }
}