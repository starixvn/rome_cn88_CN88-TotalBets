namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using Shared.ViewModels.Filters;
    using Site.ViewModels.OverView;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Features.SportTypeGroup.Models;
    using TotalBets.Shared.Constants;

    public class HomeViewModel
    {
        public HomeViewModel(
            OverViewQuery message,
            OverViewModel overViewModel,
            IEnumerable<SportGroup> allSportTypeGroup)
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
        }

        private OverViewQuery message { get; }

        public IEnumerable<OverViewByBaseViewModel> BySportType { get; }

        public OverViewItemModel Total { get; }

        public OverViewByParlayViewModel ByParlay { get; }

        public int PossType => message.PossType;

        public OverViewTitleViewModel HeaderTable { get; }

        public SingleSelectionViewModel PossTypeSelectionViewModel { get; }
    }
}