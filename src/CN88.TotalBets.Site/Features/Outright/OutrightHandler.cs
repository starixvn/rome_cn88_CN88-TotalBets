namespace CN88.TotalBets.Site.Features.Outright
{
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.Leagues;
    using TotalBets.Features.Outright;
    using TotalBets.Features.Outright.Params;
    using TotalBets.Features.Summay;
    using TotalBets.Shared.Constants;

    public class OutrightHandler : BaseHandler, IAsyncRequestHandler<OutrightQuery, OutrightViewModel>
    {
        private readonly OutrightService outrightService;
        private readonly LeagueService leagueService;
        private readonly SummaryService summaryService;

        public OutrightHandler(
            OutrightService outrightService,
            LeagueService leagueService,
            SummaryService summaryService)
        {
            this.outrightService = outrightService;
            this.leagueService = leagueService;
            this.summaryService = summaryService;
        }

        public async Task<OutrightViewModel> Handle(OutrightQuery message)
        {
            var outrightLeagues = await outrightService.GetOutrightsAsync(ToGetOutrightParam(message));
            var leagues = await leagueService.GetLeagues(message.SportTypeGroup, TotalBetTypeConstants.Outright);
            var summaries = await summaryService.GetSummaries(message.ViewerId, message.ActorLevel, TotalBetTypeConstants.Outright);

            return new OutrightViewModel(
                message,
                summaries.FirstOrDefault(x => x.SportTypeGroup == message.SportTypeGroup).SportTypeGroupName,
                outrightLeagues,
                summaries,
                BuildLeagueSelectListItems(leagues, message.LeagueId));
        }

        private static OutrightParam ToGetOutrightParam(OutrightQuery message)
            => new OutrightParam(
                message.ViewerId,
                message.ActorLevel,
                message.SportTypeGroup,
                message.LeagueId,
                message.StakeCondition,
                message.Stake,
                message.PossType);
    }
}