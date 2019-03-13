namespace CN88.TotalBets.Features.Summay.Models
{
    public class Summary
    {
        public Summary()
        {
        }

        public Summary(
            int sportTypeGroup,
            int wager,
            decimal stake)
        {
            SportTypeGroup = sportTypeGroup;
            Wager = wager;
            Stake = stake;
        }

        public int SportTypeGroup { get; }

        public string SportTypeGroupName { get; internal set; }

        public int Wager { get; }

        public decimal Stake { get; }

    }
}
