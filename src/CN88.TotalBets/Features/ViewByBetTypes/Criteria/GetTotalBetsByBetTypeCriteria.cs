namespace CN88.TotalBets.Features.ViewByBetTypes.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Shared.Constants;
    using Shared.Models;

    public class GetTotalBetsByBetTypeCriteria : CriteriaBase
    {
        public GetTotalBetsByBetTypeCriteria(
            int custId,
            UserLevel userLevel,
            int matchId,
            byte stakeCondition,
            byte marketType,
            byte possType,
            decimal stake,
            byte eventType)
        {
            CustId = custId;
            UserLevel = userLevel;
            StakeCondition = stakeCondition;
            MarketType = marketType;
            PossType = possType;
            Stake = stake;
            MatchId = matchId;
            EventType = eventType;
        }

        public int CustId { get; }

        public int MatchId { get; }

        public byte StakeCondition { get; }

        [SpParam(SqlParamName = "islive")]
        public byte MarketType { get; }

        public decimal Stake { get; }

        public byte PossType { get; }

        [SpParam(Ignored = true)]
        public byte EventType { get; }

        [SpParam(Ignored = true)]
        public UserLevel UserLevel { get; }

        public override string GetSettingKey()
        {
            if (EventType == EventTypeConstants.Started)
            {
                return UserLevel.IsSuperLevel()
                    ? "CS_TotalBets_Bettype_Started_Super"
                    : UserLevel.IsMasterLevel()
                    ? "CS_TotalBets_Bettype_Started_Master"
                    : "CS_TotalBets_Bettype_Started_Agent";
            }

            return UserLevel.IsSuperLevel()
                ? "CS_TotalBets_Bettype_Super"
                : UserLevel.IsMasterLevel()
                ? "CS_TotalBets_Bettype_Master"
                : "CS_TotalBets_Bettype_Agent";
        }

        public override bool IsValid()
            => CustId > 0 && MatchId > 0;
    }
}