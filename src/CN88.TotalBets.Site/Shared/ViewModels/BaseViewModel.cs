namespace CN88.TotalBets.Site.Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Filters;
    using Requests;
    using TotalBets.Features.Summay.Models;

    public abstract class BaseViewModel
    {
        protected readonly FilterQuery query;

        protected BaseViewModel(
            FilterQuery query,
            string title,
            IEnumerable<Summary> summaries,
            IEnumerable<SelectListItem> leagues)
        {
            this.query = query;
            ViewerId = query.ViewerId;
            ViewerLevelId = query.ViewerLevelId;
            Title = title;

            SummaryMenuViewModel = new SummaryMenuViewModel(summaries, query.SportTypeGroup);
            LeagueSelectionViewModel = new SingleSelectionViewModel(leagues, query.LeagueId);
            FilterViewModel = new FilterBaseViewModel(
               query.SportTypeGroup,
               query.StakeCondition,
               query.Stake,
               query.PossType,
               LeagueSelectionViewModel);
        }

        public int ViewerId { get; }

        public byte ViewerLevelId { get; }

        public string Title { get; }

        public string SubTitle { get; protected set; }

        public SummaryMenuViewModel SummaryMenuViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public FilterBaseViewModel FilterViewModel { get; protected set; }
    }
}