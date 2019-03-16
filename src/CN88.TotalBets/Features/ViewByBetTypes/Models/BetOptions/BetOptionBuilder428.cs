namespace CN88.TotalBets.Features.ViewByBetTypes.Models.BetOptions
{
    using System.Collections.Generic;
    using CN88.TotalBets.AppResources;
    using CN88.TotalBets.Features.Events.Models;

    public class BetOptionBuilder428 : IBetOptionBuilder
    {
        public virtual string BuildBetChoice(string betTeam, Match match, IDictionary<string, object> parameters = null)
            => betTeam?.ToLowerInvariant() == BetTeamValue.O ? Resources.Odd : Resources.Even;
    }
}