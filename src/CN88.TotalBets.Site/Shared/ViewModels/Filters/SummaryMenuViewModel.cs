namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    using System.Collections.Generic;
    using TotalBets.Features.Summay.Models;

    public class SummaryMenuViewModel
    {
        public SummaryMenuViewModel(
            IEnumerable<Summary> summaries,
            int selectedSportTypeGroup,
            string appPath)
        {
            Summaries = summaries;
            SelectedSportTypeGroup = selectedSportTypeGroup;
            AppPath = appPath;
        }

        public IEnumerable<Summary> Summaries { get; }

        public int SelectedSportTypeGroup { get; }

        public string AppPath { get; }
    }
}