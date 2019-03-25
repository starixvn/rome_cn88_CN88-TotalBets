namespace CN88.TotalBets.Site.Features.Started.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;

    public class DateSelectionViewModel
    {
        public const string Today = "1";
        public const string Yesterday = "2";

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
                new SelectListItem { Text = Resources.Today, Value = Today },
                new SelectListItem { Text = Resources.Yesterday, Value = Yesterday }
            }.Select(item =>
            {
                item.Selected = item.Value == selectedItem.ToString();
                return item;
            });
    }
}