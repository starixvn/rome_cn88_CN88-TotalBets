namespace CN88.TotalBets.Shared.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Models;

    public abstract class FilterCriteriaBase : CriteriaBase
    {
        protected FilterCriteriaBase(
            int custId,
            UserLevel userLevel,
            int sportTypeGroup,
            int leagueId,
            byte stakeCondition,
            decimal stake,
            byte possType)
        {
            CustId = custId;
            UserLevel = userLevel;
            SportTypeGroup = sportTypeGroup;
            LeagueId = leagueId;
            StakeCondition = stakeCondition;
            Stake = stake;
            PossType = possType;
        }

        public int CustId { get; }

        public int SportTypeGroup { get; }

        public int LeagueId { get; }

        public byte StakeCondition { get; }

        public decimal Stake { get; }

        public byte PossType { get; }

        [SpParam(Ignored = true)]
        public UserLevel UserLevel { get; }

        public override bool IsValid()
            => CustId > 0;
    }
}
