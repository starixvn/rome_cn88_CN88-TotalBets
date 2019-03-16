namespace CN88.TotalBets.Site.Features.ViewByBetType.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Shared.ViewModels;
    using TotalBets.Features.Events.Models;
    using TotalBets.Features.Summaries.Models;
    using TotalBets.Features.ViewByBetTypes.Models;
    using TotalBets.Shared.Constants;
    using TotalBets.Shared.Services;

    public class ViewByBetTypeViewModel : BaseViewModel
    {
        public ViewByBetTypeViewModel(
            IEnumerable<TotalBetsByBetType> totalBetsByBetTypes,
            Match match,
            SportGroupSummary summary,
            ViewByBetTypeQuery filterQuery)
            : base(filterQuery, "View By Bet Type", new Dictionary<string, object>
            {
                { nameof(filterQuery.MatchId), filterQuery.MatchId },
                { nameof(filterQuery.MarketType), filterQuery.MarketType }
            })
        {
            TotalBetsByBetTypes = totalBetsByBetTypes;
            Match = match;
            MatchSummaryMenuViewModel = new MatchSummaryMenuViewModel(
                summary,
                filterQuery.MatchId,
                $"{filterQuery.AppPath}Event?Type={filterQuery.Type}&SportTypeGroup={filterQuery.SportTypeGroup}",
                $"{filterQuery.AppPath}ViewByBetType?Type={filterQuery.Type}&SportTypeGroup={filterQuery.SportTypeGroup}");
            IsInPlay = filterQuery.Type == EventTypeConstants.InPlay;
        }

        public Match Match { get; }

        public int TotalWager => TotalBetsByBetTypes.Sum(bettype => bettype.TotalWager);

        public decimal TotalStake => TotalBetsByBetTypes.Sum(bettype => bettype.TotalStake);

        public string LeagueName => CacheRefManager.CacheRefInstance.GetLeagueName(Match.LeagueId);

        public string HomeName => CacheRefManager.CacheRefInstance.GetTeamName(Match.HomeId);

        public string AwayName => CacheRefManager.CacheRefInstance.GetTeamName(Match.AwayId);

        public IEnumerable<TotalBetsByBetType> TotalBetsByBetTypes { get; }

        public MatchSummaryMenuViewModel MatchSummaryMenuViewModel { get; }

        public bool IsInPlay { get; }
    }
}