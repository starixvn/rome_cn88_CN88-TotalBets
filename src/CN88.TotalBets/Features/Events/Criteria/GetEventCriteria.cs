namespace CN88.TotalBets.Features.Events.Criteria
{
    using Fanex.Data;
    using Params;
    using Shared.Criteria;
    using Shared.Models;

    public class GetEventCriteria : FilterCriteriaBase
    {
        public GetEventCriteria(EventParam param) : base(
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
            Type = param.Type;
            Market = param.Market;
        }

        public byte Type { get; }

        [SpParam(SqlParamName = "islive")]
        public byte Market { get; }

        public override string GetSettingKey()
            => $"CS_TotalBets_Event_{UserLevel.DisplayName}";
    }
}