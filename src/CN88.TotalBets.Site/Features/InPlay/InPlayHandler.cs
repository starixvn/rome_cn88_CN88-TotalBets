namespace CN88.TotalBets.Site.Features.InPlay
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.InPlay;
    using TotalBets.Features.Leagues;

    public class InPlayHandler : BaseHandler, IAsyncRequestHandler<InPlayQuery, InPlayViewModel>
    {
        private readonly InPlayService inPlayService;
        private readonly LeagueService leagueService;

        public InPlayHandler(InPlayService inPlayService, LeagueService leagueService)
        {
            this.inPlayService = inPlayService;
            this.leagueService = leagueService;
        }

        public async Task<InPlayViewModel> Handle(InPlayQuery message)
        {
            //var inplay = await inPlayService.GetInPlayAsync(ToGetInPlayParam(message));
            //var leagues = await leagueService.GetLeagues(message.SportTypeGroup, TotalBetTypeConstants.InPlay);

            //return new InPlayViewModel(
            //    message,
            //    inplay,
            //    BuildLeagueSelectListItems(leagues, message.LeagueId));
            return new InPlayViewModel(message, null, null);
        }

        //private static GetInPlayParam ToGetInPlayParam(InPlayQuery message)
        //    => new GetInPlayParam(
        //        message.ViewerId,
        //        message.ActorLevel,
        //        message.Type,
        //        message.SportTypeGroup,
        //        message.LeagueId,
        //        message.StakeCondition,
        //        message.Stake,
        //        message.PossType,
        //        message.IsLive);
    }
}
