namespace CN88.TotalBets.Features.Summaries.Models
{
    public class SummaryByMatchDto
    {
        public SummaryByMatchDto()
        {
        }

        public SummaryByMatchDto(
            int sportTypeGroup,
            int leagueId,
            int matchId,
            int homeId,
            int awayId,
            int wager,
            decimal stake)
        {
            SportTypeGroup = sportTypeGroup;
            LeagueId = leagueId;
            MatchId = matchId;
            HomeId = homeId;
            AwayId = awayId;
            Wager = wager;
            Stake = stake;
        }

        public int SportTypeGroup { get; }

        public int LeagueId { get; }

        public int MatchId { get; }

        public int HomeId { get; }

        public int AwayId { get; }

        public int Wager { get; }

        public decimal Stake { get; }
    }
}