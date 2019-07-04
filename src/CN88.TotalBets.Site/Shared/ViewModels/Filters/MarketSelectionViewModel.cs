namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using TotalBets.Shared.Constants;

    public class MarketSelectionViewModel
    {
        public MarketSelectionViewModel(byte selectedMarket)
        {
            SelectedMarket = selectedMarket;
            Items = BuildSelectListItems(selectedMarket);
        }

        public byte SelectedMarket { get; }

        public IEnumerable<SelectListItem> Items { get; }

        private static IEnumerable<SelectListItem> BuildSelectListItems(byte selectedItem)
            => new List<SelectListItem>
            {
                new SelectListItem { Text = Resources.AllMarkets, Value = MarketConstants.AllMarkets.ToString() },
                new SelectListItem { Text = Resources.PreMatch, Value = MarketConstants.PreMatch.ToString() },
                new SelectListItem { Text = Resources.InPlay, Value = MarketConstants.InPlay.ToString() }
            }.Select(item =>
            {
                item.Selected = item.Value == selectedItem.ToString();
                return item;
            });
    }
}