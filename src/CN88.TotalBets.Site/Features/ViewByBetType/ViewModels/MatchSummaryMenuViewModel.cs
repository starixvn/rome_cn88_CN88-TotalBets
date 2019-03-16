namespace CN88.TotalBets.Site.Features.ViewByBetType.ViewModels
{
    using TotalBets.Features.Summaries.Models;

    public class MatchSummaryMenuViewModel
    {
        public MatchSummaryMenuViewModel(SportGroupSummary summary, int selectedMatchId)
        {
            Summary = summary;
            SelectedMatchId = selectedMatchId;
        }

        public SportGroupSummary Summary { get; }

        public int SelectedMatchId { get; }
    }
}