namespace CN88.TotalBets.Site.Features.Overview
{
    using System.Threading.Tasks;
    using MediatR;
    using TotalBets.Features.Overview;
    using TotalBets.Features.SportTypeGroup;
    using ViewModels;

    public class OverViewHandler : IAsyncRequestHandler<OverViewQuery, HomeViewModel>
    {
        private readonly IOverViewService overViewService;
        private readonly SportTypeGroupService sportTypeGroupService;

        public OverViewHandler(
            IOverViewService overViewService,
            SportTypeGroupService sportTypeGroupService)
        {
            this.overViewService = overViewService;
            this.sportTypeGroupService = sportTypeGroupService;
        }

        public async Task<HomeViewModel> Handle(OverViewQuery message)
        {
            var overView = await overViewService.Get(message.ViewerId, message.PossType, message.ViewerLevelId);
            var allSportTypeGroup = await sportTypeGroupService.GetSportTypeGroups();

            return new HomeViewModel(message, overView, allSportTypeGroup, message.GetSportDisplayName);
        }
    }
}