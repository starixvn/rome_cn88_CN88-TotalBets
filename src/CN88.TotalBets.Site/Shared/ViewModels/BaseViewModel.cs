namespace CN88.TotalBets.Site.Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using Filters;
    using Requests;
    using TotalBets.Features.Summaries.Models;
    using TotalBets.Shared.Constants;

    public abstract class BaseViewModel
    {
        protected readonly FilterQuery query;

        protected BaseViewModel(
            FilterQuery query,
            string title,
            string appPath,
            IEnumerable<SportGroupSummary> summaries,
            IEnumerable<SelectListItem> leagues)
        {
            this.query = query;
            Title = title;

            SummaryMenuViewModel = new SummaryMenuViewModel(summaries, query.SportTypeGroup, $"{appPath}?Type={query.Type}");
            var possTypeSelectListItems = PossTypeConstants.PossTypeByLevel[query.ActorLevel].Select(
                x => new SelectListItem
                {
                    Text = Resources.ResourceManager.GetString(x.Value),
                    Value = x.Key.ToString(),
                    Selected = x.Key == query.PossType
                });
            PossTypeSelectionViewModel = new SingleSelectionViewModel(possTypeSelectListItems, query.PossType);
            LeagueSelectionViewModel = new SingleSelectionViewModel(leagues, query.LeagueId);

            FilterViewModel = new FilterViewModel(
                query.Type,
                query.SportTypeGroup,
                query.StakeCondition,
                query.Stake,
                query.PossType,
                query.Type == EventTypeConstants.InPlay,
                query.Market,
                PossTypeSelectionViewModel,
                LeagueSelectionViewModel);
        }

        protected BaseViewModel(
            FilterQuery filterQuery,
            string title,
            IDictionary<string, object> hiddenParams,
            bool isShowLeague = false,
            bool isShowMarket = true)
        {
            query = filterQuery;
            Title = title;

            var possTypeSelectListItems = PossTypeConstants.PossTypeByLevel[query.ActorLevel].Select(
                x => new SelectListItem
                {
                    Text = Resources.ResourceManager.GetString(x.Value),
                    Value = x.Key.ToString(),
                    Selected = x.Key == query.PossType
                });
            PossTypeSelectionViewModel = new SingleSelectionViewModel(possTypeSelectListItems, query.PossType);

            FilterViewModel = new FilterViewModel(
                query.Type,
                query.SportTypeGroup,
                query.StakeCondition,
                query.Stake,
                PossTypeSelectionViewModel,
                isShowMarket,
                query.Market,
                isShowLeague,
                hiddenParams);
        }

        public string Title { get; }

        public string SubTitle { get; protected set; }

        public SummaryMenuViewModel SummaryMenuViewModel { get; }

        public SingleSelectionViewModel PossTypeSelectionViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public FilterViewModel FilterViewModel { get; protected set; }
    }
}