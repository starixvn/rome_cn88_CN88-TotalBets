namespace CN88.TotalBets.Features.WagerAccount
{
    public class WagerAccountByEventParam : WagerAccountParam
    {
        public WagerAccountByEventParam(
            int custId,
            int matchId,
            decimal stake,
            int stakeCondition,
            byte roleId,
            int bettypeGroup,
            string betteam,
            int liveIndicator) : base(custId, matchId, stake, stakeCondition, roleId)
        {
            BettypeGroup = bettypeGroup;
            BetTeam = betteam;
            LiveIndicator = liveIndicator;
        }

        public int BettypeGroup { get; }

        public string BetTeam { get; }

        public int LiveIndicator { get; }

    }
}
