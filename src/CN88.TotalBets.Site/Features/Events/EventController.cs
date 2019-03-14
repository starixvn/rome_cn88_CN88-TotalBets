namespace CN88.TotalBets.Site.Features.Events
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Request;
    using Shared.Controllers;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class EventController : SiteBaseController
    {
        private const string ViewPath = "~/Features/Events/Views/{0}.cshtml";

        public EventController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> InPlay(InPlayQuery message)
            => await ViewAsync(message, string.Format(ViewPath, nameof(InPlay)));

        public async Task<ActionResult> Today(InPlayQuery message)
            => await ViewAsync(message, string.Format(ViewPath, nameof(Today)));

        public async Task<ActionResult> Early(InPlayQuery message)
            => await ViewAsync(message, string.Format(ViewPath, nameof(Early)));

        public async Task<ActionResult> Started(InPlayQuery message)
            => await ViewAsync(message, string.Format(ViewPath, nameof(Started)));
    }
}