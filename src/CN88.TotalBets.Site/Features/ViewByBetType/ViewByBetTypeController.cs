namespace CN88.TotalBets.Site.Features.ViewByBetType
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using CN88.TotalBets.Site.Shared.Controllers;
    using MediatR;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ViewByBetTypeController : SiteBaseController
    {
        private const string ViewPath = "~/Features/ViewByBetType/Views/Index.cshtml";

        public ViewByBetTypeController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ViewByBetTypeQuery message)
        {
            return await ViewAsync(message, ViewPath);
        }
    }
}