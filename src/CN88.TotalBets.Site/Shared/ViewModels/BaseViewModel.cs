namespace CN88.TotalBets.Site.Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using Filters;
    using Requests;
    using TotalBets.Shared.Constants;

    public abstract class BaseViewModel
    {
        protected readonly FilterQuery query;

        protected BaseViewModel(
            FilterQuery filterQuery,
            string title,
            IDictionary<string, object> hiddenParams,
            IEnumerable<SelectListItem> leagues,
            bool isShowDateSelection,
            bool isShowMarketSelection,
            bool isShowLeagueSelection)
        {
            query = filterQuery;
            Title = title;
            IsAutoRefresh = filterQuery.IsAutoRefresh;

            var possTypeSelectListItems = PossTypeConstants.PossTypeByLevel[query.ActorLevel].Select(
                x => new SelectListItem
                {
                    Text = Resources.ResourceManager.GetString(x.Value),
                    Value = x.Key.ToString(),
                    Selected = x.Key == query.PossType
                });
            PossTypeSelectionViewModel = new SingleSelectionViewModel(possTypeSelectListItems, query.PossType);
            DateSelectionViewModel = isShowDateSelection ? new DateSelectionViewModel(query.DateType) : null;
            MarketSelectionViewModel = isShowMarketSelection ? new MarketSelectionViewModel(query.Market) : null;
            LeagueSelectionViewModel = isShowLeagueSelection ? new SingleSelectionViewModel(leagues, query.LeagueId) : null;

            FilterViewModel = new FilterViewModel(
                query.SportTypeGroup,
                query.StakeCondition,
                query.Stake,
                new Dictionary<string, object>(hiddenParams)
                {
                    [nameof(filterQuery.IsAutoRefresh)] = filterQuery.IsAutoRefresh
                },
                PossTypeSelectionViewModel,
                DateSelectionViewModel,
                MarketSelectionViewModel,
                LeagueSelectionViewModel);
        }

        public string Title { get; }

        public string SubTitle { get; protected set; }

        public DateSelectionViewModel DateSelectionViewModel { get; }

        public SingleSelectionViewModel PossTypeSelectionViewModel { get; }

        public MarketSelectionViewModel MarketSelectionViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public FilterViewModel FilterViewModel { get; protected set; }

        public bool IsAutoRefresh { get; }
    }
}