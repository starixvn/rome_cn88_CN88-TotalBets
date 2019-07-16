namespace CN88.TotalBets.Site.Features.Overview
{
    using System.Threading.Tasks;
    using MediatR;
    using TotalBets.Features.Overview;
    using TotalBets.Features.Overview.Params;
    using TotalBets.Features.SportTypeGroup;
    using ViewModels;
    using Web.Helpers;

    public class OverViewHandler : IAsyncRequestHandler<OverViewQuery, HomeViewModel>
    {
        private readonly IOverViewService overViewService;
        private readonly SportTypeGroupService sportTypeGroupService;
        private readonly BrowserHelper browserHelper;

        public OverViewHandler(
            IOverViewService overViewService,
            SportTypeGroupService sportTypeGroupService,
            BrowserHelper browserHelper)
        {
            this.overViewService = overViewService;
            this.sportTypeGroupService = sportTypeGroupService;
            this.browserHelper = browserHelper;
        }

        public async Task<HomeViewModel> Handle(OverViewQuery message)
        {
            var param = new OverViewParam(
                message.ViewerId,
                message.SubAccountId,
                message.IsAllDownline,
                message.PossType,
                message.ActorLevel);
            var overView = await overViewService.Get(param);
            var allSportTypeGroup = await sportTypeGroupService.GetSportTypeGroups();

            return new HomeViewModel(message, overView, allSportTypeGroup, browserHelper);
        }
    }
}