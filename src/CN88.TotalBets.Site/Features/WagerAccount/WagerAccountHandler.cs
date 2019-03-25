namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.WagerAccount;

    public class WagerAccountHandler : BaseHandler, 
        IAsyncRequestHandler<WagerAccountByOutrightQuery, WagerAccountByOutrightViewModel>,
        IAsyncRequestHandler<WagerAccountByEventQuery, WagerAccountByEventViewModel>
    {
        private readonly IWagerAccountService wagerAccountService;

        public WagerAccountHandler(IWagerAccountService wagerAccountService)
        {
            this.wagerAccountService = wagerAccountService;
        }

        public async Task<WagerAccountByOutrightViewModel> Handle(WagerAccountByOutrightQuery message)
        {
            var param = new WagerAccountParam(
                message.CustId,
                message.MatchId,
                message.Stake,
                message.StakeCondition,
                message.CustLevelId);

            var wagerAccounts = await wagerAccountService.GetByOutright(param);

            return new WagerAccountByOutrightViewModel(wagerAccounts, message);
        }

        public async Task<WagerAccountByEventViewModel> Handle(WagerAccountByEventQuery message)
        {
            var param = new WagerAccountByEventParam(
                message.CustId,
                message.MatchId,
                message.Stake,
                message.StakeCondition,
                message.CustLevelId,
                message.BetType,
                message.BetTeam,
                message.LiveIndicator);

            var wagerAccounts = await wagerAccountService.GetByEvent(param);

            return new WagerAccountByEventViewModel(wagerAccounts, message);
        }
    }
}