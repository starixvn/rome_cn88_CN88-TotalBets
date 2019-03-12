namespace CN88.TotalBets.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Requests.Outright;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class OutrightController : SiteBaseController
    {
        public OutrightController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(OutrightQuery message)
            => await ViewAsync(message);
    }
}