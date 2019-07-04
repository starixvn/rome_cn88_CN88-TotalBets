namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using Events.Models;
    using Models;

    public class BetOptionBuilder1 : IBetOptionBuilder
    {
        public virtual string BuildBetChoice(BetOption betOption, Match match, IDictionary<string, object> parameters = null)
        {
            var betHomeTeam = betOption.BetTeam != null
                && betOption.BetTeam.ToLowerInvariant().Contains(BetTeamValue.H);

            return betHomeTeam
                ? match?.HomeName
                : match?.AwayName;
        }
    }
}