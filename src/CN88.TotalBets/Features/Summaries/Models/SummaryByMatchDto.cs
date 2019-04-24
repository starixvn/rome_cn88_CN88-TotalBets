namespace CN88.TotalBets.Features.Summaries.Models
{
    public class SummaryByMatchDto
    {
        public SummaryByMatchDto()
        {
        }

        public SummaryByMatchDto(
            int leagueId,
            int matchId,
            int homeId,
            int awayId,
            int wager,
            decimal stake,
            bool isNeutral)
        {
            LeagueId = leagueId;
            MatchId = matchId;
            HomeId = homeId;
            AwayId = awayId;
            Wager = wager;
            Stake = stake;
            IsNeutral = isNeutral;
        }

        public int LeagueId { get; }

        public int MatchId { get; }

        public int HomeId { get; }

        public int AwayId { get; }

        public int Wager { get; }

        public decimal Stake { get; }

        public bool IsNeutral { get; }
    }
}