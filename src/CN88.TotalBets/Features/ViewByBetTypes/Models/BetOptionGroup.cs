namespace CN88.TotalBets.Features.ViewByBetTypes.Models
{
    using System.Collections.Generic;

    public class BetOptionGroup
    {
        public BetOptionGroup(
            IEnumerable<BaseBetOption> betOptions,
            bool isLive)
        {
            BetOptions = betOptions;
            IsLive = isLive;
        }

        public IEnumerable<BaseBetOption> BetOptions { get; }

        public bool IsLive { get; }
    }
}