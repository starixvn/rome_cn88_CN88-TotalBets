namespace CN88.TotalBets.Features.ViewByBetTypes.Models
{
    using System.Collections.Generic;

    public class TotalBetsByBetType
    {
        public TotalBetsByBetType(
            int betTypeId,
            int totalWagers,
            decimal totalStake,
            IEnumerable<BetOptionGroup> betOptionGroups)
        {
            BetTypeId = betTypeId;
            TotalWager = totalWagers;
            TotalStake = totalStake;
            BetOptionGroups = betOptionGroups;
        }

        public int BetTypeId { get; }

        public int TotalWager { get; }

        public decimal TotalStake { get; }

        public IEnumerable<BetOptionGroup> BetOptionGroups { get; }
    }
}