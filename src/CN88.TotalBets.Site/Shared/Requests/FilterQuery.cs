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
        }

        public int SportTypeGroup { get; set; }

        public int LeagueId { get; set; }

        public byte StakeCondition { get; set; }

        public decimal Stake { get; set; }

        public byte PossType { get; set; }
    }
}