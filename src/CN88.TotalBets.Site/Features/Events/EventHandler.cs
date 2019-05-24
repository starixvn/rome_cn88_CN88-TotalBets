namespace CN88.TotalBets.Site.Features.Events
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using Shared.Helpers;
    using TotalBets.Features.Events;
    using TotalBets.Features.Events.Params;
    using TotalBets.Features.Leagues;
    using TotalBets.Features.SportTypeGroup;
    using TotalBets.Features.Summaries;
    using ViewModels;

    public class EventHandler : BaseHandler, IAsyncRequestHandler<EventQuery, EventViewModel>
    {
        private readonly EventService eventService;
        private readonly LeagueService leagueService;
        private readonly SummaryService summaryService;
        private readonly SportTypeGroupService sportTypeGroupService;
        private readonly BrowserHelper browserHelper;

        public EventHandler(
            EventService eventService,
            LeagueService leagueService,
            SummaryService summaryService,
            SportTypeGroupService sportTypeGroupService,
            BrowserHelper browserHelper)
        {
            this.eventService = eventService;
            this.leagueService = leagueService;
            this.summaryService = summaryService;
            this.sportTypeGroupService = sportTypeGroupService;
            this.browserHelper = browserHelper;
        }

        public async Task<EventViewModel> Handle(EventQuery message)
        {
            var eventModel = await eventService.GetEventsAsync(ToEventParam(message));
            var leagues = await leagueService.GetLeagues(message.SportTypeGroup, message.Type);
            var summaries = await summaryService.GetSportGroupSummaries(message.ViewerId, message.ActorLevel, message.Type);

            return new EventViewModel(
                message,
                await sportTypeGroupService.GetSportTypeGroupName(message.SportTypeGroup),
                eventModel,
                summaries,
                BuildLeagueSelectListItems(leagues, message.LeagueId),
                browserHelper);
        }

        private static EventParam ToEventParam(EventQuery message)
            => new EventParam(
                message.ViewerId,
                message.ActorLevel,
                message.Type,
                message.SportTypeGroup,
                message.LeagueId,
                message.StakeCondition,
                message.Stake,
                message.PossType,
                message.Market);
    }
}