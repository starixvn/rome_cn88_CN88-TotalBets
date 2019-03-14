namespace CN88.TotalBets.Site.Features.ViewByBetType.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using CN88.TotalBets.Features.Events.Models;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;
    using CN88.TotalBets.Shared.Services;

    public class ViewByBetTypeViewModel
    {
        public ViewByBetTypeViewModel(
            IEnumerable<TotalBetsByBetType> totalBetsByBetTypes,
            Match match)
        {
            TotalBetsByBetTypes = totalBetsByBetTypes;
            Match = match;
        }

        public Match Match { get; }

        public int TotalWager => TotalBetsByBetTypes.Sum(bettype => bettype.TotalWager);

        public decimal TotalStake => TotalBetsByBetTypes.Sum(bettype => bettype.TotalStake);

        public string LeagueName => CacheRefManager.CacheRefInstance.GetLeagueName(Match.LeagueId);

        public string HomeName => CacheRefManager.CacheRefInstance.GetTeamName(Match.HomeId);

        public string AwayName => CacheRefManager.CacheRefInstance.GetTeamName(Match.AwayId);

        public IEnumerable<TotalBetsByBetType> TotalBetsByBetTypes { get; }
    }
}