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
        public ViewByBetTypeViewModel(
            IEnumerable<TotalBetsByBetType> totalBetsByBetTypes,
            Match match,
            SportGroupSummary summary,
            ViewByBetTypeQuery filterQuery)
            : base(filterQuery, Resources.ViewByBetType, new Dictionary<string, object>
            {
                { nameof(filterQuery.MatchId), filterQuery.MatchId },
                { nameof(filterQuery.ViewFromInPlayPage), filterQuery.ViewFromInPlayPage }
            },
            false,
            filterQuery.ViewFromInPlayPage)
        {
            TotalBetsByBetTypes = totalBetsByBetTypes;
            Match = match;
            ViewFromInPlayPage = filterQuery.ViewFromInPlayPage;
            ViewFromEarlyPage = filterQuery.Type == EventTypeConstants.Early;
            MatchSummaryMenuViewModel = new MatchSummaryMenuViewModel(
                summary,
                filterQuery.MatchId,
                string.Join(
                    "&",
                    $"{filterQuery.AppPath}Event?Type={filterQuery.Type}",
                    $"SportTypeGroup={filterQuery.SportTypeGroup}",
                    $"PossType={filterQuery.PossType}",
                    $"Stake={filterQuery.Stake}",
                    $"StakeCondition={filterQuery.StakeCondition}",
                    $"Market={filterQuery.Market}"),
                string.Join(
                    "&",
                    $"{filterQuery.AppPath}ViewByBetType?Type={filterQuery.Type}",
                    $"SportTypeGroup={filterQuery.SportTypeGroup}",
                    $"ViewFromInPlayPage={filterQuery.ViewFromInPlayPage}",
                    $"PossType={filterQuery.PossType}",
                    $"Stake={filterQuery.Stake}",
                    $"StakeCondition={filterQuery.StakeCondition}",
                    $"Market={filterQuery.Market}"));
        }

        public Match Match { get; }

        public int TotalWager => TotalBetsByBetTypes.Sum(bettype => bettype.TotalWager);

        public decimal TotalStake => TotalBetsByBetTypes.Sum(bettype => bettype.TotalStake);

        public string LeagueName => CacheRefManager.CacheRefInstance.GetLeagueName(Match.LeagueId);

        public IEnumerable<TotalBetsByBetType> TotalBetsByBetTypes { get; }

        public MatchSummaryMenuViewModel MatchSummaryMenuViewModel { get; }

        public bool ViewFromInPlayPage { get; }

        public bool ViewFromEarlyPage { get; }

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
                betTeam = betTeam
            };

        public bool ShowScore(int sportTypeId)
            => sportTypeId == SportTypeConstants.Soccer && ViewFromInPlayPage;
    }
}