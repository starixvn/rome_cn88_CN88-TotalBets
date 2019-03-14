namespace CN88.TotalBets.Features.Outright.Criteria
{
    using Params;
    using Shared.Criteria;
    using Shared.Models;

    public class GetOutrightCriteria : FilterCriteriaBase
    {
        public GetOutrightCriteria(OutrightParam param) : base(
            param.CustId,
            param.UserLevel,
            param.SportTypeGroup,
            param.LeagueId,
            param.StakeCondition,
            param.Stake,
            param.PossType)
        {
        }

        public override string GetSettingKey()
            => UserLevel.IsSuperLevel()
            ? "CS_TotalBets_Outright_Super"
            : (UserLevel.IsMasterLevel() ? "CS_TotalBets_Outright_Master" : "CS_TotalBets_Outright_Agent");
    }
}