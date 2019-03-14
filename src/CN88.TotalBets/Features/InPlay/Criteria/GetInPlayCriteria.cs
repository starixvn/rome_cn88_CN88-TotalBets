namespace CN88.TotalBets.Features.InPlay.Criteria
{
    using Params;
    using Shared.Criteria;
    using Shared.Models;

    public class GetInPlayCriteria : FilterCriteriaBase
    {
        public GetInPlayCriteria(InPlayParam param) : base(
            param.CustId,
            param.UserLevel,
            param.SportTypeGroup,
            param.LeagueId,
            param.StakeCondition,
            param.Stake,
            param.PossType)
        {
            Type = param.Type;
            IsLive = param.IsLive;
        }

        public byte Type { get; }

        public byte IsLive { get; }

        public override string GetSettingKey()
            => UserLevel.IsSuperLevel()
            ? "CS_TotalBets_Event_Super"
            : (UserLevel.IsMasterLevel() ? "CS_TotalBets_Event_Master" : "CS_TotalBets_Event_Agent");
    }
}
