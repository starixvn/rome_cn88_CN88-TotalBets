namespace CN88.TotalBets.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Requests.InPlay;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class InPlayController : SiteBaseController
    {
        public InPlayController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(InPlayQuery message)
            => await ViewAsync(message);
    }
}