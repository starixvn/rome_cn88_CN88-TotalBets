namespace CN88.TotalBets.Features.Events.Models
{
    using System.Collections.Generic;

    public class MatchLeague
    {
        public MatchLeague(
            int leagueId,
            string leagueName,
            IEnumerable<Match> matches)
        {
            LeagueId = leagueId;
            LeagueName = leagueName;
            Matches = matches;
        }

        public int LeagueId { get; }

        public string LeagueName { get; }

        public IEnumerable<Match> Matches { get; }
    }
}
