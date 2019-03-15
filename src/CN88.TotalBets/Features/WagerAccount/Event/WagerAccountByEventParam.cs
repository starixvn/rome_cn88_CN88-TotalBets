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
            bool liveIndicator) : base(custId, matchId, stake, stakeCondition, roleId)
        {
            BettypeGroup = bettypeGroup;
            BetTeam = betteam;
            LiveIndicator = liveIndicator;
        }

        public int BettypeGroup { get; }

        public string BetTeam { get; }

        public bool LiveIndicator { get; }

    }
}
