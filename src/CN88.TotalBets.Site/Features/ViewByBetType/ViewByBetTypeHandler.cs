namespace CN88.TotalBets.Site.Features.ViewByBetType
{
    using System.Threading.Tasks;
    using MediatR;
    using TotalBets.Features.Events;
    using TotalBets.Features.SportTypeGroup;
    using TotalBets.Features.Summaries;
    using TotalBets.Features.ViewByBetTypes;
    using TotalBets.Features.ViewByBetTypes.Params;
    using ViewModels;

    public class ViewByBetTypeHandler : IAsyncRequestHandler<ViewByBetTypeQuery, ViewByBetTypeViewModel>
    {
        private readonly ViewByBetTypeService viewByBetTypeService;
        private readonly EventService eventService;
        private readonly SummaryService summaryService;
        private readonly SportTypeGroupService sportTypeGroupService;

        public ViewByBetTypeHandler(
            ViewByBetTypeService viewByBetTypeService,
            EventService eventService,
            SummaryService summaryService,
            SportTypeGroupService sportTypeGroupService)
        {
            this.viewByBetTypeService = viewByBetTypeService;
            this.eventService = eventService;
            this.summaryService = summaryService;
            this.sportTypeGroupService = sportTypeGroupService;
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
                message.Market,
                message.Type));

            var summary = await summaryService
                .GetSportGroupSummary(message.ViewerId, message.ActorLevel, message.Type, message.SportTypeGroup);

            return new ViewByBetTypeViewModel(
                await sportTypeGroupService.GetSportTypeGroupName(message.SportTypeGroup),
                totalBetByBetTypes.Values,
                match,
                summary,
                message);
        }
    }
}