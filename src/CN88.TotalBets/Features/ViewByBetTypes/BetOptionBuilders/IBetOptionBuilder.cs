namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using CN88.TotalBets.Features.Events.Models;

    public interface IBetOptionBuilder
    {
        string BuildBetChoice(string betTeam, Match match, IDictionary<string, object> parameters = null);
    }
}
