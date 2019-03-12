using System.Collections.Generic;
using CN88.TotalBets.Models.OverView;
using CN88.TotalBets.Site.Requests.OverView;

namespace CN88.TotalBets.Site.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel(OverViewQuery message, OverViewModel overViewModel)
        {
            this.message = message;
            BySportType = new List<OverViewByBaseViewModel>
            {
                new OverViewByInPlayViewModel(overViewModel.BySportTypeModels),
                new OverViewByTodayViewModel(overViewModel.BySportTypeModels),
                new OverViewByEarlyViewModel(overViewModel.BySportTypeModels),
                new OverViewByOutrightViewModel(overViewModel.BySportTypeModels)
            };

            Total = overViewModel.TotalOverView;
            ByParlay = new OverViewByParlayViewModel(overViewModel.ParlayModel);
        }

        private OverViewQuery message { get; }        

        public IEnumerable<OverViewByBaseViewModel> BySportType { get; }

        public OverViewItemModel Total { get; }

        public OverViewByParlayViewModel ByParlay { get; }
    }
}