namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SingleSelectionViewModel
    {
        public SingleSelectionViewModel(
            IEnumerable<SelectListItem> items,
            int userSelectedId)
        {
            Items = items;
            UserSelectedId = userSelectedId;
        }

        public IEnumerable<SelectListItem> Items { get; }

        public int UserSelectedId { get; }
    }
}
