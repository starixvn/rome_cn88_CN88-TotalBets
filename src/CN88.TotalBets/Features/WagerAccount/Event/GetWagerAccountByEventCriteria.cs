using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using Fanex.Data;
using Fanex.Data.Repository;

namespace CN88.TotalBets.Features.WagerAccount
{
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
        }

        public int CustId { get; }

        public int MatchId { get; }

        public int BettypeGroup { get; }

        public string BetTeam { get; }

        public bool LiveIndicator { get; }

        public int StakeCondition { get; }

        public decimal Stake { get; }

        [SpParam(Ignored = true)]
        public UserLevel Role { get; }

        public override string GetSettingKey()
            => Role.IsSuperLevel() ? "CS_TotalBets_WagerAccount_Event_Master" :
            Role.IsMasterLevel() ? "CS_TotalBets_WagerAccount_Event_Agent" : "CS_TotalBets_WagerAccount_Event_Member";

        public override bool IsValid()
            => CustId > 0 && 
            Stake >= 0 && 
            MatchId > 0 && 
            StakeConditionConstants.Conditions.ContainsKey(StakeCondition);
    }
}
