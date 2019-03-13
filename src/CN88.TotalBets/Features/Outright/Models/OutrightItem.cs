namespace CN88.TotalBets.Features.Outright.Models
{
    public class OutrightItem
    {
        public OutrightItem()
        {
        }

        public OutrightItem(
            int leagueId,
            int teamId,
            int wager,
            decimal stake)
        {
            LeagueId = leagueId;
            TeamId = teamId;
            Wager = wager;
            Stake = stake;
        }

        public int LeagueId { get; }

        public int TeamId { get; }

        public string TeamName { get; set; }

        public int Wager { get; }

        public decimal Stake { get; }
    }
}