namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    using System.Collections.Generic;
    using TotalBets.Features.Summaries.Models;

    public class SummaryMenuViewModel
    {
        public SummaryMenuViewModel(
            IEnumerable<SportGroupSummary> summaries,
            int selectedSportTypeGroup,
            string appPath)
        {
            Summaries = summaries;
            SelectedSportTypeGroup = selectedSportTypeGroup;
            AppPath = appPath;
        }

        public IEnumerable<SportGroupSummary> Summaries { get; }

        public int SelectedSportTypeGroup { get; }

        public string AppPath { get; }
    }
}