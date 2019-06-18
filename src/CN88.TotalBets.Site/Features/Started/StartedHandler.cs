namespace CN88.TotalBets.Site.Features.Started
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.Leagues;
    using TotalBets.Features.SportTypeGroup;
    using TotalBets.Features.Started;
    using TotalBets.Features.Started.Params;
    using TotalBets.Features.Summaries;
    using TotalBets.Shared.Constants;
    using ViewModels;
    using Web.Helpers;

    public class StartedHandler : BaseHandler, IAsyncRequestHandler<StartedQuery, StartedViewModel>
    {
        private readonly StartedService startedService;
        private readonly LeagueService leagueService;
        private readonly SummaryService summaryService;
        private readonly SportTypeGroupService sportTypeGroupService;
        private readonly BrowserHelper browserHelper;

        public StartedHandler(
            StartedService startedService,
            LeagueService leagueService,
            SummaryService summaryService,
            SportTypeGroupService sportTypeGroupService,
            BrowserHelper browserHelper)
        {
            this.startedService = startedService;
            this.leagueService = leagueService;
            this.summaryService = summaryService;
            this.sportTypeGroupService = sportTypeGroupService;
            this.browserHelper = browserHelper;
        }

        public async Task<StartedViewModel> Handle(StartedQuery message)
        {
            var data = await startedService.GetStarted(
                new StartedParam(
                    message.ViewerId,
                    message.ActorLevel,
                    message.SportTypeGroup,
                    message.LeagueId,
                    message.StakeCondition,
                    message.Stake,
                    message.PossType,
                    message.DateType,
                    message.Market));

            var leagues = await leagueService.GetLeagues(message.SportTypeGroup, EventTypeConstants.Started);
            var summaries = await summaryService.GetSportGroupSummaries(message.ViewerId, message.ActorLevel, EventTypeConstants.Started);

            return new StartedViewModel(
                message,
                await sportTypeGroupService.GetSportTypeGroupName(message.SportTypeGroup),
                data,
                summaries,
                BuildLeagueSelectListItems(leagues, message.LeagueId),
                browserHelper);
        }
    }
}