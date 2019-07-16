namespace CN88.TotalBets.Features.WagerAccount
{
    public class WagerAccountByEventParam : WagerAccountParam
    {
        public WagerAccountByEventParam(
            int custId,
            int subAccId,
            bool isAllDownline,
            int matchId,
            decimal stake,
            int stakeCondition,
            byte roleId,
            int bettypeGroup,
            string betteam,
            int liveIndicator,
            byte eventType)
            : base(custId, subAccId, isAllDownline, matchId, stake, stakeCondition, roleId)
        {
            BettypeGroup = bettypeGroup;
            BetTeam = betteam;
            LiveIndicator = liveIndicator;
            EventType = eventType;
        }

        public int BettypeGroup { get; }

        public string BetTeam { get; }

        public int LiveIndicator { get; }

        public byte EventType { get; }
    }
}