namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using Shared.Helpers;
    using Shared.ViewModels.Filters;
    using Site.ViewModels.OverView;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Features.SportTypeGroup.Models;
    using TotalBets.Shared.Constants;

    public class HomeViewModel
    {
        private readonly BrowserHelper browserHelper;

        public HomeViewModel(
            OverViewQuery message,
            OverViewModel overViewModel,
            IEnumerable<SportGroup> allSportTypeGroup,
            BrowserHelper browserHelper)
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
                allSportTypeGroup);

            var possTypeSelectListItems = PossTypeConstants.PossTypeByLevel[message.ActorLevel].Select(
               x => new SelectListItem
               {
                   Text = Resources.ResourceManager.GetString(x.Value),
                   Value = x.Key.ToString(),
                   Selected = x.Key == message.PossType
               });
            PossTypeSelectionViewModel = new SingleSelectionViewModel(possTypeSelectListItems, message.PossType);
            this.browserHelper = browserHelper;
        }

        private OverViewQuery message { get; }

        public IEnumerable<OverViewByBaseViewModel> BySportType { get; }

        public OverViewItemModel Total { get; }

        public OverViewByParlayViewModel ByParlay { get; }

        public int PossType => message.PossType;

        public OverViewTitleViewModel HeaderTable { get; }

        public SingleSelectionViewModel PossTypeSelectionViewModel { get; }

        public bool IsDevices => browserHelper.IsMobile() && !browserHelper.IsTablet();

        public OverViewItemModel GetTotal(int colIndex)
        {
            int wager = 0;
            decimal stake = 0;
            for (var i = 0; i < BySportType.Count(); i++)
            {
                var row = BySportType.ElementAt(i);
                var sportColumn = colIndex == -1 ? row.Total : row.SportsColumn[colIndex];
                wager += sportColumn.Wager;
                stake += sportColumn.Stake;
            }

            return new OverViewItemModel(colIndex, wager, stake);
        }
    }
}