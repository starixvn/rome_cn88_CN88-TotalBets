namespace CN88.TotalBets.Features.ViewByBetTypes.Models
{
    using System.Collections.Generic;
    using CN88.TotalBets.AppResources;

    public class BetOptionGroup
    {
        public BetOptionGroup(
            IEnumerable<BaseBetOption> betOptions,
            bool isLive)
        {
            BetOptions = betOptions;
            IsLive = isLive;
            LiveText = IsLive ? Resources.InPlay: Resources.PreMatch;
        }

        public IEnumerable<BaseBetOption> BetOptions { get; }

        public bool IsLive { get; }

        public string LiveText { get; }
    }
}