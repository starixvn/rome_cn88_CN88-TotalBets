namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Shared.Controllers;
    using Web.Helpers;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class WagerAccountController : SiteBaseController
    {
        private const string OutrightViewPath = "~/Features/WagerAccount/Outright/Views/Index.cshtml";
        private const string EventViewPath = "~/Features/WagerAccount/Event/Views/Index.cshtml";
        private const string OutrightMobileViewPath = "~/Features/WagerAccount/Outright/Views/IndexMobile.cshtml";
        private const string EventMobileViewPath = "~/Features/WagerAccount/Event/Views/IndexMobile.cshtml";
        private readonly BrowserHelper browserHelper;

        public WagerAccountController(IMediator mediator, BrowserHelper browserHelper)
            : base(mediator)
        {
            this.browserHelper = browserHelper;
        }

        public async Task<ActionResult> GetByOutright(WagerAccountByOutrightQuery message)
        {
            message.CurrentUrl = Request.Url.Query;

            return await ViewAsync(message,
                this.browserHelper.IsMobile()
                ? OutrightMobileViewPath
                : OutrightViewPath);
        }

        public async Task<ActionResult> GetByEvent(WagerAccountByEventQuery message)
        {
            message.CurrentUrl = Request.Url.Query;

            return await ViewAsync(message,
                this.browserHelper.IsMobile()
                ? EventMobileViewPath
                : EventViewPath);
        }
    }
}