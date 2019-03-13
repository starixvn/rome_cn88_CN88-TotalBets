namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Site.ViewModels.OverView;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Features.SportTypeGroup.Models;

    public class HomeViewModel
    {
        public HomeViewModel(
            OverViewQuery message,
            OverViewModel overViewModel,
            IEnumerable<SportGroup> allSportTypeGroup,
            Func<int, string> getSportDisplayName)
        {
            this.message = message;
            BySportType = new List<OverViewByBaseViewModel>
            {
                new OverViewByInPlayViewModel(overViewModel.BySportTypeModels),
                new OverViewByTodayViewModel(overViewModel.BySportTypeModels),
                new OverViewByEarlyViewModel(overViewModel.BySportTypeModels),
                new OverViewByOutrightViewModel(overViewModel.BySportTypeModels)
            };

            Total = overViewModel.Total;
            ByParlay = new OverViewByParlayViewModel(overViewModel.ParlayModel);
            HeaderTable = new OverViewTitleViewModel(
                overViewModel.BySportTypeModels.Select(i => i.SportTypeGroup),
                allSportTypeGroup,
                getSportDisplayName);
        }

        private OverViewQuery message { get; }

        public IEnumerable<OverViewByBaseViewModel> BySportType { get; }

        public OverViewItemModel Total { get; }

        public OverViewByParlayViewModel ByParlay { get; }

        public int PossType => message.PossType;

        public OverViewTitleViewModel HeaderTable { get; }
    }
}