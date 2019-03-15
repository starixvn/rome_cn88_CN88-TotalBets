namespace CN88.TotalBets.Site.Features.ViewByBetType
{
    using System.Threading.Tasks;
    using CN88.TotalBets.Features.Events;
    using CN88.TotalBets.Features.ViewByBetTypes;
    using CN88.TotalBets.Features.ViewByBetTypes.Params;
    using CN88.TotalBets.Site.Features.ViewByBetType.ViewModels;
    using MediatR;

    public class ViewByBetTypeHandler : IAsyncRequestHandler<ViewByBetTypeQuery, ViewByBetTypeViewModel>
    {
        private readonly ViewByBetTypeService viewByBetTypeService;
        private readonly EventService eventService;

        public ViewByBetTypeHandler(
            ViewByBetTypeService viewByBetTypeService,
            EventService eventService)
        {
            this.viewByBetTypeService = viewByBetTypeService;
            this.eventService = eventService;
        }

        public async Task<ViewByBetTypeViewModel> Handle(ViewByBetTypeQuery message)
        {
            var match = await eventService.GetMatch(message.MatchId);

            var totalBetByBetTypes = await viewByBetTypeService.GetBetTypes(
                new ViewByBetTypeParam(
                message.CustId,
                message.ActorLevel,
                message.MatchId,
                message.StakeCondition,
                message.Stake,
                message.PossType,
                message.MarketType));

            return new ViewByBetTypeViewModel(totalBetByBetTypes.Values, match, message);
        }
    }
}