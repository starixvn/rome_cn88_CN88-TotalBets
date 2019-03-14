namespace CN88.TotalBets.Features.ViewByBetTypes.Criteria
{
    using CN88.TotalBets.Shared.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetTotalBetsByBetTypeCriteria : CriteriaBase
    {
        public GetTotalBetsByBetTypeCriteria(
            int custId,
            UserLevel userLevel,
            int matchId,
            byte stakeCondition,
            byte marketType,
            byte possType,
            decimal stake)
        {
            CustId = custId;
            UserLevel = userLevel;
            StakeCondition = stakeCondition;
            MarketType = marketType;
            PossType = possType;
            Stake = stake;
            MatchId = matchId;
        }

        public int CustId { get; }

        public int MatchId { get; }

        public byte StakeCondition { get; }

        [SpParam(SqlParamName = "islive")]
        public byte MarketType { get; }

        public decimal Stake { get; }

        public byte PossType { get; }

        [SpParam(Ignored = true)]
        public UserLevel UserLevel { get; }

        public override string GetSettingKey()
            => UserLevel.IsSuperLevel()
            ? "CS_TotalBets_Bettype_Super"
#pragma warning disable S3358 // Ternary operators should not be nested
            : UserLevel.IsMasterLevel()
                ? "CS_TotalBets_Bettype_Master"
                : "CS_TotalBets_Bettype_Agent";

#pragma warning restore S3358 // Ternary operators should not be nested

        public override bool IsValid()
            => CustId > 0 & MatchId > 0;
    }
}