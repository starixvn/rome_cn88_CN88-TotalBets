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
        private const string ViewPath = "~/Features/Events/Views/Index.cshtml";

        public EventController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(EventQuery message)
            => await ViewAsync(message, ViewPath);
    }
}