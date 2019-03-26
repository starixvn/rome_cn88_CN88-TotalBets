using System.Collections.Generic;
using System.Linq;

namespace CN88.TotalBets.Features.Started.Models
{
    public class StartedLeague
    {
        public StartedLeague(
            int leagueId,
            string leagueName,
            IEnumerable<StartedMatch> matches)
        {
            LeagueId = leagueId;
            LeagueName = LeagueName;
            Matches = matches;
        }

        public int LeagueId { get; }

        public int LeagueName { get; }

        public IEnumerable<StartedMatch> Matches { get; }

        public int TotalPMWager => Matches.Sum(m => m.PMWager);

        public decimal TotalPMStake => Matches.Sum(m => m.PMStake);

        public int TotalIPWager => Matches.Sum(m => m.IPWager);

        public decimal TotalIPStake => Matches.Sum(m => m.IPStake);
    }
}