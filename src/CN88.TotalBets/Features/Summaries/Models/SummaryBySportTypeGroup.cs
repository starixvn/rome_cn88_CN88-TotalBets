namespace CN88.TotalBets.Features.Summaries.Models
{
    public class SummaryBySportTypeGroup
    {
        public int SportTypeGroup { get; }

        public string SportTypeGroupName { get; internal set; }

        public int LeagueId { get; }

        public string LeagueName { get; internal set; }

        public int MatchId { get; }

        public int HomeId { get; }

        public string HomeTeamName { get; internal set; }

        public int AwayId { get; }

        public string AwayTeamName { get; internal set; }
    }
}