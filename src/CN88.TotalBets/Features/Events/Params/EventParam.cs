namespace CN88.TotalBets.Features.Events.Params
{
    using Shared.Models;
    using Shared.Params;

    public class EventParam : FilterParam
    {
        public EventParam(
            int custId,
            UserLevel userLevel,
            byte type,
            int sportTypeGroup,
            int leagueId,
            byte stakeCondition,
            decimal stake,
            byte possType,
            byte market) : base(custId, userLevel, sportTypeGroup, leagueId, stakeCondition, stake, possType)
        {
            Type = type;
            Market = market;
        }

        public byte Type { get; }

        public byte Market { get; }
    }
}