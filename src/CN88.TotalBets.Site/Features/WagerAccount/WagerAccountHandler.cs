namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.WagerAccount;
    using Web.Helpers;

    public class WagerAccountHandler : BaseHandler,
        IAsyncRequestHandler<WagerAccountByOutrightQuery, WagerAccountByOutrightViewModel>,
        IAsyncRequestHandler<WagerAccountByEventQuery, WagerAccountByEventViewModel>
    {
        private readonly IWagerAccountService wagerAccountService;
        private readonly BrowserHelper browserHelper;

        public WagerAccountHandler(
            IWagerAccountService wagerAccountService,
            BrowserHelper browserHelper)
        {
            this.wagerAccountService = wagerAccountService;
            this.browserHelper = browserHelper;
        }

        public async Task<WagerAccountByOutrightViewModel> Handle(WagerAccountByOutrightQuery message)
        {
            var isMobile = browserHelper.IsMobile();
            var isAllDownline = message.IsAllDownline
                || message.ActorLevel > message.CustLevel;
            var param = new WagerAccountParam(
                message.CustId,
                message.SubAccountId,
                isAllDownline,
                message.MatchId,
                message.Stake,
                message.StakeCondition,
                message.CustLevelId);

            var wagerAccounts = await wagerAccountService.GetByOutright(param);

            return new WagerAccountByOutrightViewModel(wagerAccounts, message, isMobile);
        }

        public async Task<WagerAccountByEventViewModel> Handle(WagerAccountByEventQuery message)
        {
            var isMobile = browserHelper.IsMobile();
            var isAllDownline = message.IsAllDownline
                || message.ActorLevel > message.CustLevel;
            var param = new WagerAccountByEventParam(
                message.CustId,
                message.SubAccountId,
                isAllDownline,
                message.MatchId,
                message.Stake,
                message.StakeCondition,
                message.CustLevelId,
                message.BetType,
                message.BetTeam,
                message.LiveIndicator,
                message.Type);

            var wagerAccounts = await wagerAccountService.GetByEvent(param);

            return new WagerAccountByEventViewModel(wagerAccounts, message, isMobile);
        }
    }
}