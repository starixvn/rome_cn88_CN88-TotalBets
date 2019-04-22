namespace CN88.TotalBets.Site.Features.ViewByBetType.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using AppResources;
    using Shared.ViewModels;
    using TotalBets.Features.Events.Models;
    using TotalBets.Features.Summaries.Models;
    using TotalBets.Features.ViewByBetTypes.Models;
    using TotalBets.Shared.Constants;
    using TotalBets.Shared.Services;

    public class ViewByBetTypeViewModel : BaseViewModel
    {
        private readonly ViewByBetTypeQuery filterQuery;

        public ViewByBetTypeViewModel(
            string sportGroupName,
            IEnumerable<TotalBetsByBetType> totalBetsByBetTypes,
            Match match,
            SportGroupSummary summary,
            ViewByBetTypeQuery filterQuery)
            : base(
                  filterQuery,
                  title: sportGroupName,
                  hiddenParams: new Dictionary<string, object>
                  {
                      [nameof(filterQuery.Type)] = filterQuery.Type,
                      [nameof(filterQuery.MatchId)] = filterQuery.MatchId,
                      [nameof(filterQuery.IsShowMarketSelection)] = filterQuery.IsShowMarketSelection,
                      [nameof(filterQuery.IsShowKODate)] = filterQuery.IsShowKODate
                  },
                  leagues: null,
                  isShowDateSelection: false,
                  isShowMarketSelection: filterQuery.IsShowMarketSelection,
                  isShowLeagueSelection: false)
        {
            this.filterQuery = filterQuery;
            SubTitle= Resources.ResourceManager.GetString(EventTypeConstants.EventTypes[filterQuery.Type]);

            TotalBetsByBetTypes = totalBetsByBetTypes;
            Match = match;
            IsShowMarketStatus = filterQuery.IsShowMarketSelection;
            IsShowKODate = filterQuery.IsShowKODate;
            MatchSummaryMenuViewModel = new MatchSummaryMenuViewModel(
                summary,
                filterQuery.MatchId,
                BuildSummaryPath(filterQuery),
                BuildViewByBetTypePath(filterQuery));
        }

        public Match Match { get; }

        public int TotalWager => TotalBetsByBetTypes.Sum(bettype => bettype.TotalWager);

        public decimal TotalStake => TotalBetsByBetTypes.Sum(bettype => bettype.TotalStake);

        public string LeagueName => CacheRefManager.CacheRefInstance.GetLeagueName(Match.LeagueId);

        public IEnumerable<TotalBetsByBetType> TotalBetsByBetTypes { get; }

        public MatchSummaryMenuViewModel MatchSummaryMenuViewModel { get; }

        public bool IsShowMarketStatus { get; }

        public bool IsShowKODate { get; }

        public object GetWagerAccountParams(int bettypeId, string betTeam, bool liveIndicator)
            => new
            {
                custId = query.ViewerId,
                custLevelId = query.ViewerLevelId,
                custName = query.ViewerName,
                matchId = Match.MatchId,
                Stake = FilterViewModel.Stake,
                StakeCondition = FilterViewModel.StakeCondition,
                BetType = bettypeId,
                liveIndicator = liveIndicator ? 1 : 0,
                betTeam = betTeam,
                type = filterQuery.Type
            };

        public bool ShowScore(int sportTypeId)
            => sportTypeId == SportTypeConstants.Soccer && IsShowMarketStatus;

        private string BuildSummaryPath(ViewByBetTypeQuery filterQuery)
        {
            if (filterQuery.Type == EventTypeConstants.Started)
            {
                return Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc(
                    $"{filterQuery.AppPath}Started",
                    GetRouteValues(filterQuery));
            }

            return Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc(
                    $"{filterQuery.AppPath}Event",
                    new Dictionary<string, object>(GetRouteValues(filterQuery))
                    {
                        [nameof(filterQuery.Type)] = filterQuery.Type
                    });
        }

        private string BuildViewByBetTypePath(ViewByBetTypeQuery filterQuery)
            => Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc(
                $"{filterQuery.AppPath}ViewByBetType",
                new Dictionary<string, object>(GetRouteValues(filterQuery))
                {
                    [nameof(filterQuery.Type)] = filterQuery.Type,
                    [nameof(filterQuery.IsShowKODate)] = filterQuery.IsShowKODate
                });

        private IDictionary<string, object> GetRouteValues(ViewByBetTypeQuery filterQuery)
            => new Dictionary<string, object>
            {
                [nameof(filterQuery.SportTypeGroup)] = filterQuery.SportTypeGroup,
                [nameof(filterQuery.PossType)] = filterQuery.PossType,
                [nameof(filterQuery.Stake)] = filterQuery.Stake,
                [nameof(filterQuery.StakeCondition)] = filterQuery.StakeCondition,
                [nameof(filterQuery.Market)] = filterQuery.Market,
                [nameof(filterQuery.LeagueId)] = filterQuery.LeagueId,
                [nameof(filterQuery.DateType)] = filterQuery.DateType
            };
    }
}