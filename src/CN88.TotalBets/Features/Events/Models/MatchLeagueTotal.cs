namespace CN88.TotalBets.Features.Events.Models
{
    using System.Collections.Generic;

    public class MatchLeagueTotal
    {
        public MatchLeagueTotal(
            byte type,
            IEnumerable<MatchLeague> matchLeagues,
            int totalWager,
            decimal totalStake)
        {
            Type = type;
            MatchLeagues = matchLeagues;
            TotalWager = totalWager;
            TotalStake = totalStake;
        }

        public byte Type { get; }

        public IEnumerable<MatchLeague> MatchLeagues { get; }

        public int TotalWager { get; }

        public decimal TotalStake { get; }
    }
}
