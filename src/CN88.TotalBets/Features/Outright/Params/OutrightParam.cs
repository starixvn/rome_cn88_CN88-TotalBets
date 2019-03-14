namespace CN88.TotalBets.Features.Outright.Params
{
    using Shared.Models;
    using Shared.Params;

    public class OutrightParam : FilterParam
    {
        public OutrightParam(
            int custId,
            UserLevel userLevel,
            int sportTypeGroup,
            int leagueId,
            byte stakeCondition,
            decimal stake,
            byte possType) : base(custId, userLevel, sportTypeGroup, leagueId, stakeCondition, stake, possType)
        {
        }
    }
}