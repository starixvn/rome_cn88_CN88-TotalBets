namespace CN88.TotalBets.Features.WagerAccount
{
    using CN88.TotalBets.Shared.Models;

    public class WagerAccountParam
    {
        public WagerAccountParam(
            int custId,
            int subAccId,
            bool isAllDownline,
            int matchId,
            decimal stake,
            int stakeCondition,
            byte roleId)
        {
            CustId = custId;
            SubAccId = subAccId;
            IsAllDownline = isAllDownline;
            MatchId = matchId;
            Stake = stake;
            Role = Enumeration.FromValue<UserLevel>(roleId);
            StakeCondition = stakeCondition;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsAllDownline { get; }

        public int MatchId { get; }

        public int StakeCondition { get; }

        public decimal Stake { get; }

        public UserLevel Role { get; }
    }
}