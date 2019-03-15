namespace CN88.TotalBets.Features.Summaries.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class LeagueSummary
    {
        public LeagueSummary(
            int leagueId,
            string leagueName,
            IEnumerable<MatchSummary> matchSummaries)
        {
            LeagueId = leagueId;
            LeagueName = leagueName;
            MatchSummaries = matchSummaries;
        }

        public int LeagueId { get; }

        public string LeagueName { get; }

        public IEnumerable<MatchSummary> MatchSummaries { get; }

        public int Wagers => MatchSummaries.Select(s => s.Wager).Sum();

        public decimal Stake => MatchSummaries.Select(s => s.Stake).Sum();
    }
}