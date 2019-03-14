namespace CN88.TotalBets.Features.ViewByBetTypes.Params
{
    using CN88.TotalBets.Shared.Models;
    using CN88.TotalBets.Shared.Params;

    public class ViewByBetTypeParam : FilterParam
    {
        public ViewByBetTypeParam(
            int custId,
            UserLevel userLevel,
            int matchId,
            byte stakeCondition,
            decimal stake,
            byte possType,
            byte marketType) : base(custId, userLevel, 0, 0, stakeCondition, stake, possType)
        {
            MarketType = marketType;
            MatchId = matchId;
        }

        public byte MarketType { get; }

        public int MatchId { get; }
    }
}