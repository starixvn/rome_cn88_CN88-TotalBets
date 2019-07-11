namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using AppResources;
    using Events.Models;
    using Models;

    public class BetOptionBuilder3 : IBetOptionBuilder
    {
        public virtual string BuildBetChoice(BetOption betOption, Match match, IDictionary<string, object> parameters = null)
            => BetTeamValue.H == betOption?.BetTeam?.ToLowerInvariant() ? Resources.Over : Resources.Under;
    }
}