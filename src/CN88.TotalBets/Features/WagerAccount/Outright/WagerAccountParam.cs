namespace CN88.TotalBets.Features.WagerAccount
{
    using CN88.TotalBets.Shared.Models;

    public class WagerAccountParam
    {
        public WagerAccountParam(
            int custId,
            int matchId,
            decimal stake,
            int stakeCondition,
            byte roleId)
        {
            CustId = custId;
            MatchId = matchId;
            Stake = stake;
            Role = Enumeration.FromValue<UserLevel>(roleId);
            StakeCondition = stakeCondition;
        }

        public int CustId { get; }

        public int MatchId { get; }

        public int StakeCondition { get; }

        public decimal Stake { get; }

        public UserLevel Role { get; }
    }
}
