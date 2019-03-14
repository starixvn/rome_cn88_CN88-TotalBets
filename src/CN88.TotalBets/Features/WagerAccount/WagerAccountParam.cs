using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CN88.TotalBets.Shared.Models;

namespace CN88.TotalBets.Features.WagerAccount
{
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
