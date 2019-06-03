namespace CN88.TotalBets.Features.WagerAccount
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Shared.Constants;
    using Shared.Models;

    public class GetWagerAccountCriteria : CriteriaBase
    {
        public GetWagerAccountCriteria(WagerAccountParam param)
        {
            CustId = param.CustId;
            MatchId = param.MatchId;
            StakeCondition = param.StakeCondition;
            Stake = param.Stake;
            Role = param.Role;
        }

        public int CustId { get; }

        public int MatchId { get; }

        public int StakeCondition { get; }

        public decimal Stake { get; }

        [SpParam(Ignored = true)]
        public UserLevel Role { get; }

        public override string GetSettingKey()
            => $"CS_TotalBets_WagerAccount_Outright_{Role.GetDirectDownLineLevel().DisplayName}";

        public override bool IsValid()
            => CustId > 0 &&
            Stake >= 0 &&
            MatchId > 0 &&
            StakeConditionConstants.Conditions.ContainsKey(StakeCondition);
    }
}