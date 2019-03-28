namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using AppResources;
    using Events.Models;
    using Models;
    using Shared.Services;

    public class DefaultBetOptionBuilder : IBetOptionBuilder
    {
        public string BuildBetChoice(BetOption betOption, Match match, IDictionary<string, object> parameters = null)
        {
            var isShowMarketStatus = parameters != null && parameters.ContainsKey("isShowMarketStatus")
                ? (bool?)parameters["isShowMarketStatus"]
                : false;

            var betTypeName = CacheRefManager.CacheRefInstance.GetBetTypeName(betOption.BetTypeId);

            return
                isShowMarketStatus.HasValue && isShowMarketStatus.Value
                ? string.Join(
                string.Empty,
                betTypeName,
                " ",
                "(",
                betOption.IsLive ? Resources.InPlay : Resources.PreMatch,
                ")")
                : betTypeName;
        }
    }
}