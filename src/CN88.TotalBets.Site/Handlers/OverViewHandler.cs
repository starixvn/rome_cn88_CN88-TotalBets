using CN88.TotalBets.Services;
using CN88.TotalBets.Site.Requests.OverView;
using CN88.TotalBets.Site.ViewModels.OverView;
using MediatR;

namespace CN88.TotalBets.Site.Handlers
{
    public class OverViewHandler : IRequestHandler<OverViewQuery, OverViewViewModel>
    {
        private readonly IOverViewService overViewService;

        public OverViewHandler(IOverViewService overViewService)
        {
            this.overViewService = overViewService;
        }

        public OverViewViewModel Handle(OverViewQuery message)
        {
            var overView = overViewService.Get(message.ViewerId, message.ViewMode, message.ViewerLevelId);

            return new OverViewViewModel(message, overView);
        }
    }
}