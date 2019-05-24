namespace CN88.TotalBets.Features.WagerAccount
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Shared.Constants;
    using Shared.Models;

    public class GetWagerAccountByEventCriteria : CriteriaBase
    {
        public GetWagerAccountByEventCriteria(WagerAccountByEventParam param)
        {
            CustId = param.CustId;
            MatchId = param.MatchId;
            StakeCondition = param.StakeCondition;
            Stake = param.Stake;
            Role = param.Role;
            BetTeam = param.BetTeam;
            BettypeGroup = param.BettypeGroup;
            LiveIndicator = param.LiveIndicator;
            EventType = param.EventType;
        }

        public int CustId { get; }

        public int MatchId { get; }

        public int BettypeGroup { get; }

        public string BetTeam { get; }

        public int LiveIndicator { get; }

        public int StakeCondition { get; }

        public decimal Stake { get; }

        [SpParam(Ignored = true)]
        public byte EventType { get; }

        [SpParam(Ignored = true)]
        public UserLevel Role { get; }

        public override string GetSettingKey()
        {
            if (EventType == EventTypeConstants.Started)
            {
                return Role.IsSuperLevel()
                    ? "CS_TotalBets_WagerAccount_Started_Master"
                    : Role.IsMasterLevel()
                    ? "CS_TotalBets_WagerAccount_Started_Agent"
                    : "CS_TotalBets_WagerAccount_Started_Member";
            }

            return Role.IsSuperLevel()
                ? "CS_TotalBets_WagerAccount_Event_Master"
                : Role.IsMasterLevel()
                ? "CS_TotalBets_WagerAccount_Event_Agent"
                : "CS_TotalBets_WagerAccount_Event_Member";
        }

        public override bool IsValid()
            => CustId > 0 &&
            Stake >= 0 &&
            MatchId > 0 &&
            StakeConditionConstants.Conditions.ContainsKey(StakeCondition);
    }
}