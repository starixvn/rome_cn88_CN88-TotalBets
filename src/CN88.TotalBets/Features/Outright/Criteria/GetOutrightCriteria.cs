namespace CN88.TotalBets.Features.Outright.Criteria
{
    using Params;
    using Shared.Criteria;
    using Shared.Models;

    public class GetOutrightCriteria : FilterCriteriaBase
    {
        public GetOutrightCriteria(OutrightParam param) : base(
            param.CustId,
            param.SubAccId,
            param.IsAllDownline,
            param.UserLevel,
            param.SportTypeGroup,
            param.LeagueId,
            param.StakeCondition,
            param.Stake,
            param.PossType)
        {
        }

        public override string GetSettingKey()
            => $"CS_TotalBets_Outright_{UserLevel.DisplayName}";
    }
}