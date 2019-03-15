namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.WagerAccount;

    public class WagerAccountHandler : BaseHandler, IAsyncRequestHandler<WagerAccountQuery, WagerAccountViewModel>
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

            var wagerAccounts = await wagerAccountService.Get(param);

            return new WagerAccountViewModel(wagerAccounts, message);
        }
    }
}