namespace CN88.TotalBets.Features.Events.Params
{
    using Shared.Models;
    using Shared.Params;

    public class InPlayParam : FilterParam
    {
        public InPlayParam(
            int custId,
            UserLevel userLevel,
            int sportTypeGroup,
            int leagueId,
            byte stakeCondition,
            decimal stake,
            byte possType,
            byte isLive) : base(custId, userLevel, sportTypeGroup, leagueId, stakeCondition, stake, possType)
        {
            IsLive = isLive;
        }

        public byte IsLive { get; }
    }
}
