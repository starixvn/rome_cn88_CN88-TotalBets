namespace CN88.TotalBets.Features.ViewByBetTypes.Params
{
    using Shared.Models;
    using Shared.Params;

    public class ViewByBetTypeParam : FilterParam
    {
        public ViewByBetTypeParam(
            int custId,
            UserLevel userLevel,
            int matchId,
            byte stakeCondition,
            decimal stake,
            byte possType,
            byte marketType,
            byte eventType) 
            : base(custId, userLevel, 0, 0, stakeCondition, stake, possType)
        {
            MarketType = marketType;
            MatchId = matchId;
            EventType = eventType;
        }

        public byte MarketType { get; }

        public int MatchId { get; }

        public byte EventType { get; }
    }
}