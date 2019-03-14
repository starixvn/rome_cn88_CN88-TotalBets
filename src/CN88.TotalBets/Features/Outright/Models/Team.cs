namespace CN88.TotalBets.Features.Outright.Models
{
    public class Team
    {
        public Team()
        {
        }

        public Team(
            int leagueId,
            int matchId,
            int teamId,
            int wager,
            decimal stake)
        {
            LeagueId = leagueId;
            MatchId = matchId;
            TeamId = teamId;
            Wager = wager;
            Stake = stake;
        }

        public int LeagueId { get; }

        public int MatchId { get; }

        public int TeamId { get; }

        public string TeamName { get; set; }

        public int Wager { get; }

        public decimal Stake { get; }
    }
}