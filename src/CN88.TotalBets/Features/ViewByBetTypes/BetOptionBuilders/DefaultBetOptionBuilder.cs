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
        {
            var viewFromInPlayPage = parameters != null && parameters.ContainsKey("viewFromInPlayPage")
                ? (bool?)parameters["viewFromInPlayPage"]
                : false;

            var betTypeName = CacheRefManager.CacheRefInstance.GetBetTypeName(betOption.BetTypeId);

            return
                viewFromInPlayPage.HasValue && viewFromInPlayPage.Value
                ? betTypeName
                : string.Join(
                string.Empty,
                betTypeName,
                " ",
                "(",
#pragma warning disable S3358 // Ternary operators should not be nested
                betOption.IsLive ? Resources.InPlay : Resources.PreMatch,
#pragma warning restore S3358 // Ternary operators should not be nested
                ")");
        }
    }
}