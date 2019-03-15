namespace CN88.TotalBets.Site.Shared.Requests
{
    using TotalBets.Shared.Constants;
    using TotalBets.Shared.Models;

    public abstract class FilterQuery : BaseQuery
    {
        protected FilterQuery(IUser viewer)
            : base(viewer)
        {
            SportTypeGroup = SportTypeConstants.Soccer;
            StakeCondition = StakeConditionConstants.GreaterThan;
            PossType = PossTypeConstants.Full_Percentage;
            Market = MarketConstants.AllMarkets;
        }

        public byte Type { get; set; }

        public int SportTypeGroup { get; set; }

        public int LeagueId { get; set; }

        public byte StakeCondition { get; set; }

        public decimal Stake { get; set; }

        public byte Market { get; set; }

        public byte PossType { get; set; }
    }
}