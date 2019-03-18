namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using CN88.TotalBets.Features.Events.Models;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;

    public interface IBetOptionBuilder
    {
        string BuildBetChoice(BetOption betOption, Match match, IDictionary<string, object> parameters = null);
    }
}
