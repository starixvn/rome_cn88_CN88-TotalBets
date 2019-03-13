namespace CN88.TotalBets.Models.Outright
{
    using System.Collections.Generic;

    public class OutrightLeague
    {
        public OutrightLeague(
            int leagueId,
            string leagueName,
            IEnumerable<OutrightItem> outrights,
            int totalWager,
            decimal totalStake)
        {
            LeagueId = leagueId;
            LeagueName = leagueName;
            Outrights = outrights;
            TotalWager = totalWager;
            TotalStake = totalStake;
        }

        public int LeagueId { get; }

        public string LeagueName { get; }

        public IEnumerable<OutrightItem> Outrights { get; }

        public int TotalWager { get; }

        public decimal TotalStake { get; }
    }
}
