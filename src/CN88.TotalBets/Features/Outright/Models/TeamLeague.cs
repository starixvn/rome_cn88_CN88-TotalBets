namespace CN88.TotalBets.Features.Outright.Models
{
    using System.Collections.Generic;

    public class TeamLeague
    {
        public TeamLeague(
            int leagueId,
            string leagueName,
            IEnumerable<Team> teams,
            int totalWager,
            decimal totalStake)
        {
            LeagueId = leagueId;
            LeagueName = leagueName;
            Teams = teams;
            TotalWager = totalWager;
            TotalStake = totalStake;
        }

        public int LeagueId { get; }

        public string LeagueName { get; }

        public IEnumerable<Team> Teams { get; }

        public int TotalWager { get; }

        public decimal TotalStake { get; }
    }
}