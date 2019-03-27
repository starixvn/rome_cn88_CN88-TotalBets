namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using TotalBets.Shared.Constants;

    public class DateSelectionViewModel
    {
        public DateSelectionViewModel(byte selectedItem)
        {
            SelectedItem = selectedItem;
            Items = BuildSelectListItems(selectedItem);
        }

        public byte SelectedItem { get; }

        public IEnumerable<SelectListItem> Items { get; }

        private IEnumerable<SelectListItem> BuildSelectListItems(byte selectedItem)
            => new List<SelectListItem>
            {
                new SelectListItem { Text = Resources.Today, Value = DateTypeConstants.Today.ToString() },
                new SelectListItem { Text = Resources.Yesterday, Value = DateTypeConstants.Yesterday.ToString() }
            }.Select(item =>
            {
                item.Selected = item.Value == selectedItem.ToString();
                return item;
            });
    }
}