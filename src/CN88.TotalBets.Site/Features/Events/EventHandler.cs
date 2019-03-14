namespace CN88.TotalBets.Site.Features.Events
{
    using System.Threading.Tasks;
    using MediatR;
    using Shared.Handlers;
    using TotalBets.Features.Events;
    using TotalBets.Features.Events.Params;
    using TotalBets.Features.Leagues;
    using TotalBets.Features.Summay;
    using TotalBets.Shared.Constants;
    using ViewModels;

    public class EventHandler : BaseHandler, IAsyncRequestHandler<EventQuery, EventViewModel>
    {
        private readonly EventService eventService;
        private readonly LeagueService leagueService;
        private readonly SummaryService summaryService;

        public EventHandler(
            EventService eventService,
            LeagueService leagueService,
            SummaryService summaryService)
        {
            this.eventService = eventService;
            this.leagueService = leagueService;
            this.summaryService = summaryService;
        }

        public async Task<EventViewModel> Handle(EventQuery message)
        {
            var eventModel = await eventService.GetEventsAsync(ToEventParam(message));
            var leagues = await leagueService.GetLeagues(message.SportTypeGroup, TotalBetTypeConstants.InPlay);
            var summaries = await summaryService.GetSummaries(message.ViewerId, message.ActorLevel, TotalBetTypeConstants.Outright);

            return new EventViewModel(
                message,
                eventModel,
                summaries,
                BuildLeagueSelectListItems(leagues, message.LeagueId));
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
                message.IsLive);
    }
}
