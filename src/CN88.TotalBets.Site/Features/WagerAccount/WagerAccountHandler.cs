namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.WagerAccount;

    public class WagerAccountHandler : BaseHandler, 
        IAsyncRequestHandler<WagerAccountQuery, WagerAccountViewModel>,
        IAsyncRequestHandler<WagerAccountByEventQuery, WagerAccountByEventViewModel>
    {
        private readonly IWagerAccountService wagerAccountService;

        public WagerAccountHandler(IWagerAccountService wagerAccountService)
        {
            this.wagerAccountService = wagerAccountService;
        }

        public async Task<WagerAccountViewModel> Handle(WagerAccountQuery message)
        {
            var param = new WagerAccountParam(
                message.CustId,
                message.MatchId,
                message.Stake,
                message.StakeCondition,
                message.CustLevelId);

            var wagerAccounts = await wagerAccountService.GetOutright(param);

            return new WagerAccountViewModel(wagerAccounts, message);
        }

        public async Task<WagerAccountByEventViewModel> Handle(WagerAccountByEventQuery message)
        {
            var param = new WagerAccountByEventParam(
                message.CustId,
                message.MatchId,
                message.Stake,
                message.StakeCondition,
                message.CustLevelId,
                message.BetTypeGroup,
                message.BetTeam,
                message.LiveIndicator);

            var wagerAccounts = await wagerAccountService.GetEvent(param);

            return new WagerAccountByEventViewModel(wagerAccounts, message);
        }
    }
}