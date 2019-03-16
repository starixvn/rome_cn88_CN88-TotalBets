namespace CN88.TotalBets.Features.ViewByBetTypes.Models.BetOptions
{
    using System.Collections.Generic;
    using CN88.TotalBets.AppResources;
    using CN88.TotalBets.Features.Events.Models;

    public class BetOptionBuilder430 : IBetOptionBuilder
    {
        public virtual string BuildBetChoice(string betTeam, Match match, IDictionary<string, object> parameters = null)
        {
            switch (betTeam?.ToLowerInvariant())
            {
                case BetTeamValue.NumberOne:
                    return Resources.TwoHDotOne;

                case BetTeamValue.NumberTwo:
                    return Resources.TwoHDotTwo;

                case BetTeamValue.X:
                    return Resources.TwoHDotX;

                default:
                    return string.Empty;
            }
        }
    }
}