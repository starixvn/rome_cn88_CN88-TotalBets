namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using AppResources;
    using Events.Models;
    using Models;

    public class BetOptionBuilder18 : IBetOptionBuilder
    {
        public virtual string BuildBetChoice(BetOption betOption, Match match, IDictionary<string, object> parameters = null)
            => betOption?.BetTeam?.ToLowerInvariant() == BetTeamValue.O
            ? Resources.Over
            : Resources.Under;
    }
}