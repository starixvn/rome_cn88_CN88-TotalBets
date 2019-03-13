using System.Threading.Tasks;
using CN88.TotalBets.Services;
using CN88.TotalBets.Site.Requests.OverView;
using CN88.TotalBets.Site.ViewModels;
using MediatR;

namespace CN88.TotalBets.Site.Handlers
{
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
            var allSportTypeGroup = await sportTypeGroupService.GetSportGroups();

            return new HomeViewModel(message, overView, allSportTypeGroup, message.GetSportDisplayName);
        }
    }
}