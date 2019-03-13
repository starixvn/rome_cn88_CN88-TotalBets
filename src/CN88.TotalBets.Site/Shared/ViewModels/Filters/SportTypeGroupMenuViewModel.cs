namespace CN88.TotalBets.Site.ViewModels.Filters
{
    using System.Collections.Generic;
    using TotalBets.Models.OverView;

    public class SportTypeGroupMenuViewModel
    {
        public SportTypeGroupMenuViewModel(
            IEnumerable<OverViewBySportTypeModel> sportTypeGroups,
            int selectedSportTypeGroup)
        {
            SportTypeGroups = sportTypeGroups;
            SelectedSportTypeGroup = selectedSportTypeGroup;
        }

        public IEnumerable<OverViewBySportTypeModel> SportTypeGroups { get; }

        public int SelectedSportTypeGroup { get; }
    }
}