namespace CN88.TotalBets.Features.Events.Criteria
{
    using Params;
    using Shared.Constants;
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
            IsLive = param.IsLive;
        }

        public byte Type { get; } = TotalBetTypeConstants.InPlay;

        public byte IsLive { get; }

        public override string GetSettingKey()
            => UserLevel.IsSuperLevel()
            ? "CS_TotalBets_Event_Super"
            : (UserLevel.IsMasterLevel() ? "CS_TotalBets_Event_Master" : "CS_TotalBets_Event_Agent");
    }
}
