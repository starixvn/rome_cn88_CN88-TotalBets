namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    using System.Collections.Generic;
    using TotalBets.Features.Summay.Models;

    public class SummaryMenuViewModel
    {
        public SummaryMenuViewModel(IEnumerable<Summary> summaries, int selectedSportTypeGroup)
        {
            Summaries = summaries;
            SelectedSportTypeGroup = selectedSportTypeGroup;
        }

        public IEnumerable<Summary> Summaries { get; }

        public int SelectedSportTypeGroup { get; }
    }
}