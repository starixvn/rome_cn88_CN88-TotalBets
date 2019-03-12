namespace CN88.TotalBets.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Requests.OverView;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class InPlayController : SiteBaseController
    {
        public InPlayController(IMediator mediator)
            : base(mediator)
        {
        }

        public ActionResult Index(OverViewQuery message)
            => View();
    }
}