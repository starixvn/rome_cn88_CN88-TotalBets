namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using CN88.TotalBets.AppResources;
    using CN88.TotalBets.Features.Events.Models;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;

    public class BetOptionBuilder18 : IBetOptionBuilder
    {
        public virtual string BuildBetChoice(string betTeam, Match match, IDictionary<string, object> parameters = null)
            => betTeam?.ToLowerInvariant() == BetTeamValue.O
            ? Resources.Over
            : Resources.Under;
    }
}