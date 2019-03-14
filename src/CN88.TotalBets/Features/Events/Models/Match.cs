namespace CN88.TotalBets.Features.Events.Models
{
    public class Match
    {
        public Match()
        {
        }

        public Match(
            int leagueId,
            int matchId,
            string showTime,
            int homeId,
            int awayId,
            int liveHomeScore,
            int liveAwayScore,
            int wager,
            decimal stake,
            int betTypeCount)
        {
            LeagueId = leagueId;
            MatchId = matchId;
            ShowTime = showTime;
            HomeId = homeId;
            AwayId = awayId;
            LiveHomeScore = liveHomeScore;
            LiveAwayScore = liveAwayScore;
            Wager = wager;
            Stake = stake;
            BetTypeCount = betTypeCount;
        }

        public int LeagueId { get; }

        public int MatchId { get; }

        public string ShowTime { get; }

        public int HomeId { get; }

        public int AwayId { get; }

        public string Event { get; internal set; }

        public int LiveHomeScore { get; }

        public int LiveAwayScore { get; }

        public int Wager { get; }

        public decimal Stake { get; }

        public int BetTypeCount { get; }
    }
}
