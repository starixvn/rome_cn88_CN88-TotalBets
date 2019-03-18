namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using CN88.TotalBets.AppResources;
    using CN88.TotalBets.Features.Events.Models;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;

    public class BetOptionBuilder428 : IBetOptionBuilder
    {
        public virtual string BuildBetChoice(BetOption betOption, Match match, IDictionary<string, object> parameters = null)
            => betOption?.BetTeam?.ToLowerInvariant() == BetTeamValue.O ? Resources.Odd : Resources.Even;
    }
}