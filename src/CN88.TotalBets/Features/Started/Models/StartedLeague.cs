namespace CN88.TotalBets.Features.Started.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class StartedLeague
    {
        public StartedLeague(
            int leagueId,
            string leagueName,
            IEnumerable<StartedMatch> matches)
        {
            LeagueId = leagueId;
            LeagueName = leagueName;
            Matches = matches;
        }

        public int LeagueId { get; }

        public string LeagueName { get; }

        public IEnumerable<StartedMatch> Matches { get; }

        public int TotalPMWager => Matches.Sum(m => m.PMWager);

        public decimal TotalPMStake => Matches.Sum(m => m.PMStake);

        public int TotalIPWager => Matches.Sum(m => m.IPWager);

        public decimal TotalIPStake => Matches.Sum(m => m.IPStake);
    }
}