namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using CN88.TotalBets.Features.Events.Models;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;

    public class BetOptionBuilder1 : IBetOptionBuilder
    {
        public virtual string BuildBetChoice(string betTeam, Match match, IDictionary<string, object> parameters = null)
        {
            var betHomeTeam = betTeam != null
                && betTeam.ToLowerInvariant().Contains(BetTeamValue.H);

            return betHomeTeam
                ? match?.HomeName
                : match?.AwayName;
        }
    }
}