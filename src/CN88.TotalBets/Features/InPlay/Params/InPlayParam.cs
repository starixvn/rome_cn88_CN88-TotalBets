namespace CN88.TotalBets.Features.InPlay.Params
{
    using Shared.Models;
    using Shared.Params;

    public class InPlayParam : FilterParam
    {
        public InPlayParam(
            int custId,
            UserLevel userLevel,
            byte type,
            int sportTypeGroup,
            int leagueId,
            byte stakeCondition,
            decimal stake,
            byte possType,
            byte isLive) : base(custId, userLevel, sportTypeGroup, leagueId, stakeCondition, stake, possType)
        {
            Type = type;
            IsLive = isLive;
        }

        public byte Type { get; }

        public byte IsLive { get; }
    }
}
