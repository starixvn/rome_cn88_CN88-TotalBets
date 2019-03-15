namespace CN88.TotalBets.Site.Features.Outright
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.Leagues;
    using TotalBets.Features.Outright;
    using TotalBets.Features.Outright.Params;
    using TotalBets.Features.SportTypeGroup;
    using TotalBets.Features.Summaries;
    using TotalBets.Shared.Constants;

    public class OutrightHandler : BaseHandler, IAsyncRequestHandler<OutrightQuery, OutrightViewModel>
    {
        private readonly OutrightService outrightService;
        private readonly LeagueService leagueService;
        private readonly SummaryService summaryService;
        private readonly SportTypeGroupService sportTypeGroupService;

        public OutrightHandler(
            OutrightService outrightService,
            LeagueService leagueService,
            SummaryService summaryService,
            SportTypeGroupService sportTypeGroupService)
        {
            this.outrightService = outrightService;
            this.leagueService = leagueService;
            this.summaryService = summaryService;
            this.sportTypeGroupService = sportTypeGroupService;
        }

        public async Task<OutrightViewModel> Handle(OutrightQuery message)
        {
            var outrightLeagues = await outrightService.GetOutrightsAsync(ToOutrightParam(message));
            var leagues = await leagueService.GetLeagues(message.SportTypeGroup, EventTypeConstants.Outright);
            var summaries = await summaryService.GetSummaryByEventType(message.ViewerId, message.ActorLevel, EventTypeConstants.Outright);

            return new OutrightViewModel(
                message,
                await sportTypeGroupService.GetSportTypeGroupName(message.SportTypeGroup),
                outrightLeagues,
                summaries,
                BuildLeagueSelectListItems(leagues, message.LeagueId));
        }

        private static OutrightParam ToOutrightParam(OutrightQuery message)
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