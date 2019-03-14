namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.Leagues;
    using TotalBets.Features.Outright;
    using TotalBets.Features.Outright.Params;
    using TotalBets.Features.Summay;
    using TotalBets.Features.WagerAccount;
    using TotalBets.Shared.Constants;
    using ViewModels;

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