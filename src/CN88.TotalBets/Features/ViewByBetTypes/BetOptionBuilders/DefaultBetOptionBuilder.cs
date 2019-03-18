namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using CN88.TotalBets.AppResources;
    using CN88.TotalBets.Features.Events.Models;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;
    using CN88.TotalBets.Shared.Services;

    public class DefaultBetOptionBuilder : IBetOptionBuilder
    {
        public string BuildBetChoice(BetOption betOption, Match match, IDictionary<string, object> parameters = null)
            => string.Join(
                string.Empty,
                CacheRefManager.CacheRefInstance.GetBetTypeName(betOption.BetTypeId),
                " ",
                "(",
                betOption.IsLive ? Resources.InPlay : Resources.PreMatch,
                ")");
    }
}