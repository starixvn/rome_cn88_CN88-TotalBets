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
            SubAccId = param.SubAccId;
            IsAllDownline = param.IsAllDownline;
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

        public int SubAccId { get; }

        public bool IsAllDownline { get; }

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
            => EventType == EventTypeConstants.Started
                ? $"CS_TotalBets_WagerAccount_Started_{Role.GetDirectDownLineLevel().DisplayName}"
                : $"CS_TotalBets_WagerAccount_Event_{Role.GetDirectDownLineLevel().DisplayName}";

        public override bool IsValid()
            => CustId > 0 &&
            Stake >= 0 &&
            MatchId > 0 &&
            StakeConditionConstants.Conditions.ContainsKey(StakeCondition);
    }
}