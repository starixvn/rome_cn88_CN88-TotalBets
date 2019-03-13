namespace CN88.TotalBets.Site.Handlers
{
    using System.Threading.Tasks;
    using Constants;
    using MediatR;
    using Params;
    using Requests.Outright;
    using Services;
    using ViewModels.Outright;

    public class OutrightHandler : BaseHandler, IAsyncRequestHandler<OutrightQuery, OutrightViewModel>
    {
        private readonly OutrightService outrightService;
        private readonly LeagueService leagueService;

        public OutrightHandler(OutrightService outrightService, LeagueService leagueService)
        {
            this.outrightService = outrightService;
            this.leagueService = leagueService;
        }

        public async Task<OutrightViewModel> Handle(OutrightQuery message)
        {
            var outrightLeagues = await outrightService.GetOutrightsAsync(ToGetOutrightParam(message));
            var leagues = await leagueService.GetLeagues(message.SportTypeGroup, TotalBetTypeConstants.Outright);

            return new OutrightViewModel(message, outrightLeagues, BuildLeagueSelectListItems(leagues, message.LeagueId));
        }

        private static GetOutrightParam ToGetOutrightParam(OutrightQuery message)
            => new GetOutrightParam(
                message.ViewerId,
                message.ActorLevel,
                message.SportTypeGroup,
                message.LeagueId,
                message.StakeCondition,
                message.Stake,
                message.PossType);
    }
}