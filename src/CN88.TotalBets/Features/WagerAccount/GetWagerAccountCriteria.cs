using CN88.TotalBets.Shared.Models;
using Fanex.Data;
using Fanex.Data.Repository;

namespace CN88.TotalBets.Features.WagerAccount
{
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

        [SpParam(SqlParamName = "OrId")]
        public int MatchId { get; }

        public int StakeCondition { get; }

        public decimal Stake { get; }

        [SpParam(Ignored = true)]
        public UserLevel Role { get; }

        public override string GetSettingKey()
            => Role.IsMasterLevel() ? "CS_TotalBets_WagerAccount_Outright_Master" :
            Role.IsAgentLevel() ? "CS_TotalBets_WagerAccount_Outright_Agent" : "CS_TotalBets_WagerAccount_Outright_Member";

        public override bool IsValid()
            => CustId > 0 && Stake >= 0 && MatchId > 0;
    }
}
