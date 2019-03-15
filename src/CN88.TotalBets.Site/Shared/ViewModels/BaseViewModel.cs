namespace CN88.TotalBets.Site.Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Filters;
    using Requests;
    using TotalBets.Features.Summay.Models;
    using TotalBets.Shared.Constants;

    public abstract class BaseViewModel
    {
        protected readonly FilterQuery query;

        protected BaseViewModel(
            FilterQuery query,
            string title,
            string appPath,
            IEnumerable<Summary> summaries,
            IEnumerable<SelectListItem> leagues)
        {
            this.query = query;
            Title = title;

            SummaryMenuViewModel = new SummaryMenuViewModel(summaries, query.SportTypeGroup, $"{appPath}?Type={query.Type}");
            LeagueSelectionViewModel = new SingleSelectionViewModel(leagues, query.LeagueId);
            FilterViewModel = new FilterViewModel(
                query.Type,
                query.SportTypeGroup,
                query.StakeCondition,
                query.Stake,
                query.PossType,
                query.Type == EventTypeConstants.InPlay,
                query.Market,
                LeagueSelectionViewModel);
        }

        public string Title { get; }

        public string SubTitle { get; protected set; }

        public SummaryMenuViewModel SummaryMenuViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public FilterViewModel FilterViewModel { get; protected set; }
    }
}