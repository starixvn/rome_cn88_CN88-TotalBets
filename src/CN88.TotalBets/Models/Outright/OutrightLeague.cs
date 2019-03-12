namespace CN88.TotalBets.Models.Outright
{
    using System.Collections.Generic;

    public class OutrightLeague
    {
        public OutrightLeague(
            int leagueId,
            string leagueName,
            IEnumerable<OutrightItem> outrights)
        {
            LeagueId = leagueId;
            LeagueName = leagueName;
            Outrights = outrights;
        }

        public int LeagueId { get; }

        public string LeagueName { get; }

        public IEnumerable<OutrightItem> Outrights { get; }
    }
}
