using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using CN88.TotalBets.Models;
using CN88.TotalBets.Models.OverView;
using CN88.TotalBets.Site.Requests.OverView;
using CN88.TotalBets.Site.ViewModels.Filters;
using CN88.TotalBets.Site.ViewModels.OverView;

namespace CN88.TotalBets.Site.ViewModels
{
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

            Total = overViewModel.TotalOverView;
            ByParlay = new OverViewByParlayViewModel(overViewModel.ParlayModel);
            HeadTable = new OverViewTitleViewModel(
                overViewModel.BySportTypeModels.Select(i => i.SportTypeGroup),
                allSportTypeGroup,
                getSportDisplayName);
        }

        private OverViewQuery message { get; }

        public IEnumerable<OverViewByBaseViewModel> BySportType { get; }

        public OverViewItemModel Total { get; }

        public OverViewByParlayViewModel ByParlay { get; }

        public int UserSelectedViewMode => message.viewModeSelectedId;

        public OverViewTitleViewModel HeadTable { get; }

    }
}