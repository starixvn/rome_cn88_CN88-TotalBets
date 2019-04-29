namespace CN88.TotalBets.Features.Events.Models
{
    using System.Collections.Generic;

    public class MatchLeague
    {
        public MatchLeague(
            int leagueId,
            string leagueName,
            IEnumerable<Match> matches,
            int totalWager,
            decimal totalStake)
        {
            LeagueId = leagueId;
            LeagueName = leagueName;
            Matches = matches;
            TotalWager = totalWager;
            TotalStake = totalStake;
        }

        public int LeagueId { get; }

        public string LeagueName { get; }

        public IEnumerable<Match> Matches { get; }

        public int TotalWager { get; }

        public decimal TotalStake { get; }
    }
}