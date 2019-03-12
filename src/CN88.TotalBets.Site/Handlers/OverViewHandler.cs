using CN88.TotalBets.Services;
using CN88.TotalBets.Site.Requests.OverView;
using CN88.TotalBets.Site.ViewModels;
using MediatR;

namespace CN88.TotalBets.Site.Handlers
{
    public class OverViewHandler : IRequestHandler<OverViewQuery, HomeViewModel>
    {
        private readonly IOverViewService overViewService;

        public OverViewHandler(IOverViewService overViewService)
        {
            this.overViewService = overViewService;
        }

        public HomeViewModel Handle(OverViewQuery message)
        {
            var overView = overViewService.Get(message.ViewerId, message.ViewMode, message.ViewerLevelId);

            return new HomeViewModel(message, overView);
        }
    }
}