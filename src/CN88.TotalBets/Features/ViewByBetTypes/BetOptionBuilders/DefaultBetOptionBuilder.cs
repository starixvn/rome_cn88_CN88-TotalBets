namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using AppResources;
    using Events.Models;
    using Models;
    using Shared.Services;

    public class DefaultBetOptionBuilder : IBetOptionBuilder
    {
        private const string isShowMarketStatusKey = "isShowMarketStatus";

        public string BuildBetChoice(BetOption betOption, Match match, IDictionary<string, object> parameters = null)
        {
            var isShowMarketStatus = parameters != null && parameters.ContainsKey(isShowMarketStatusKey)
                ? (bool?)parameters[isShowMarketStatusKey]
                : false;

            var betTypeName = CacheRefManager.CacheRefInstance.GetBetTypeName(betOption.BetTypeId);
            var isLiveText = betOption.IsLive ? Resources.InPlay : Resources.PreMatch;

            return
                isShowMarketStatus.HasValue && isShowMarketStatus.Value
                ? $"{betTypeName} ({isLiveText})"
                : betTypeName;
        }
    }
}