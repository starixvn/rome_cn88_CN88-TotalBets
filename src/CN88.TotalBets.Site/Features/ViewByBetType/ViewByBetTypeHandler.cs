namespace CN88.TotalBets.Site.Features.ViewByBetType
{
    using System.Threading.Tasks;
    using MediatR;
    using TotalBets.Features.Events;
    using TotalBets.Features.Summaries;
    using TotalBets.Features.ViewByBetTypes;
    using TotalBets.Features.ViewByBetTypes.Params;
    using ViewModels;

    public class ViewByBetTypeHandler : IAsyncRequestHandler<ViewByBetTypeQuery, ViewByBetTypeViewModel>
    {
        private readonly ViewByBetTypeService viewByBetTypeService;
        private readonly EventService eventService;
        private readonly SummaryService summaryService;

        public ViewByBetTypeHandler(
            ViewByBetTypeService viewByBetTypeService,
            EventService eventService,
            SummaryService summaryService)
        {
            this.viewByBetTypeService = viewByBetTypeService;
            this.eventService = eventService;
            this.summaryService = summaryService;
        }

        public async Task<ViewByBetTypeViewModel> Handle(ViewByBetTypeQuery message)
        {
            var match = await eventService.GetMatch(message.MatchId);

            var totalBetByBetTypes = await viewByBetTypeService.GetBetTypes(
                new ViewByBetTypeParam(
                message.ViewerId,
                message.ActorLevel,
                message.MatchId,
                message.StakeCondition,
                message.Stake,
                message.PossType,
                message.MarketType));

            var summary = await summaryService.GetSportGroupSummary(message.ViewerId, message.ActorLevel, message.Type, message.SportTypeGroup);

            return new ViewByBetTypeViewModel(totalBetByBetTypes.Values, match, summary, message);
        }
    }
}