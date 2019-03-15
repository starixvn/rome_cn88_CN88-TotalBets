namespace CN88.TotalBets.Features.Summaries.Models
{
    public class MatchSummary
    {
        public MatchSummary(
            int matchId,
            int homeId,
            string homeTeamName,
            int awayId,
            string awayTeamName,
            int wager,
            decimal stake)
        {
            MatchId = matchId;
            HomeId = homeId;
            HomeTeamName = homeTeamName;
            AwayId = awayId;
            AwayTeamName = awayTeamName;
            Wager = wager;
            Stake = stake;
        }

        public int MatchId { get; }

        public int HomeId { get; }

        public string HomeTeamName { get; }

        public int AwayId { get; }

        public string AwayTeamName { get; }

        public int Wager { get; }

        public decimal Stake { get; }
    }
}