namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using CN88.TotalBets.AppResources;
    using CN88.TotalBets.Features.Events.Models;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;

    public class BetOptionBuilder5 : IBetOptionBuilder
    {
        public virtual string BuildBetChoice(string betTeam, Match match, IDictionary<string, object> parameters = null)
        {
            switch (betTeam?.ToLowerInvariant())
            {
                case BetTeamValue.NumberOne:
                    return match?.HomeName;

                case BetTeamValue.NumberTwo:
                    return match?.AwayName;

                case BetTeamValue.X:
                    return Resources.Draw;

                default:
                    return betTeam;
            }
        }
    }
}