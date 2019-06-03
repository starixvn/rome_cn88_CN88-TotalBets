namespace CN88.TotalBets.Features.Started.Criteria
{
    using Fanex.Data;
    using Params;
    using Shared.Criteria;
    using Shared.Models;

    public class GetStartedCriteria : FilterCriteriaBase
    {
        public GetStartedCriteria(StartedParam param) : base(
            param.CustId,
            param.UserLevel,
            param.SportTypeGroup,
            param.LeagueId,
            param.StakeCondition,
            param.Stake,
            param.PossType)
        {
            DateType = param.DateType;
            Market = param.Market;
        }

        public byte DateType { get; }

        [SpParam(SqlParamName = "islive")]
        public byte Market { get; }

        public override string GetSettingKey()
            => $"CS_TotalBets_Started_{UserLevel.DisplayName}";
    }
}