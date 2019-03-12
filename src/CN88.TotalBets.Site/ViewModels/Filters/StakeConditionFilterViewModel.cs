namespace CN88.TotalBets.Site.ViewModels.Filters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class StakeConditionFilterViewModel
    {
        public StakeConditionFilterViewModel(
            IEnumerable<SelectListItem> conditions,
            int selectedCondition,
            int stake)
        {
            Conditions = conditions;
            SelectedCondition = selectedCondition;
            Stake = stake;
        }

        public IEnumerable<SelectListItem> Conditions { get; }

        public int SelectedCondition { get; }

        public int Stake { get; }
    }
}