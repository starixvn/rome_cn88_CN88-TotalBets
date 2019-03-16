namespace CN88.TotalBets.Site.Features.ViewByBetType.ViewModels
{
    using TotalBets.Features.Summaries.Models;

    public class MatchSummaryMenuViewModel
    {
        public MatchSummaryMenuViewModel(
            SportGroupSummary summary,
            int selectedMatchId,
            string summaryPath,
            string viewByBetTypePath)
        {
            Summary = summary;
            SelectedMatchId = selectedMatchId;
            SummaryPath = summaryPath;
            ViewByBetTypePath = viewByBetTypePath;
        }

        public SportGroupSummary Summary { get; }

        public int SelectedMatchId { get; }

        public string SummaryPath { get; }

        public string ViewByBetTypePath { get; }
    }
}