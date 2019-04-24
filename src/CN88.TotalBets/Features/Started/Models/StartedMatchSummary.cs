namespace CN88.TotalBets.Features.Started.Models
{
    public class StartedMatchSummary
    {
        public StartedMatchSummary()
        {
        }

        public StartedMatchSummary(
            int matchId,
            bool liveIndicator,
            int wager,
            decimal stake)
        {
            MatchId = matchId;
            LiveIndicator = liveIndicator;
            Wager = wager;
            Stake = stake;
        }

        public int MatchId { get; }

        public bool LiveIndicator { get; }

        public int Wager { get; }

        public decimal Stake { get; }
    }
}