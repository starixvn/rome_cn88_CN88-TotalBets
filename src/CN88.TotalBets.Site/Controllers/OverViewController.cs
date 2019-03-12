namespace CN88.TotalBets.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Requests.OverView;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class OverViewController : SiteBaseController
    {
        public OverViewController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(OverViewQuery message)
            => await ViewAsync(message);
    }
}