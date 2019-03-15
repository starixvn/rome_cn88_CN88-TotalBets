namespace CN88.TotalBets.Features.Summaries.Models
{
    public class SummaryByEvent
    {
        public SummaryByEvent()
        {
        }

        public SummaryByEvent(
            byte sportTypeGroup,
            int wager,
            decimal stake)
        {
            SportTypeGroup = sportTypeGroup;
            Wager = wager;
            Stake = stake;
        }

        public byte SportTypeGroup { get; }

        public string SportTypeGroupName { get; internal set; }

        public int Wager { get; }

        public decimal Stake { get; }
    }
}