namespace CN88.TotalBets.Site.Handlers
{
    using System.Threading.Tasks;
    using MediatR;
    using Params;
    using Requests.Outright;
    using Services;
    using ViewModels.Outright;

    public class OutrightHandler : IAsyncRequestHandler<OutrightQuery, OutrightViewModel>
    {
        private readonly OutrightService outrightService;

        public OutrightHandler(OutrightService outrightService)
        {
            this.outrightService = outrightService;
        }

        public async Task<OutrightViewModel> Handle(OutrightQuery message)
        {
            var outrights = await outrightService.GetOutrightsAsync(ToGetOutrightParam(message));

            return new OutrightViewModel(message.ViewerId, message.ActorLevel, outrights);
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