namespace CN88.TotalBets.Features.Started.Params
{
    using Shared.Models;
    using Shared.Params;

    public class StartedParam : FilterParam
    {
        public StartedParam(
            int custId,
            int subAccId,
            bool isAllDownline,
            UserLevel userLevel,
            int sportTypeGroup,
            int leagueId,
            byte stakeCondition,
            decimal stake,
            byte possType,
            byte dateType,
            byte market)
            : base(custId, subAccId, isAllDownline, userLevel, sportTypeGroup, leagueId, stakeCondition, stake, possType)
        {
            DateType = dateType;
            Market = market;
        }

        public byte DateType { get; }

        public byte Market { get; }
    }
}