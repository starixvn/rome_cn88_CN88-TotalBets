namespace CN88.TotalBets.Site.Features.Outright
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Shared.ViewModels.Filters;
    using TotalBets.Features.Outright.Models;
    using TotalBets.Features.Summay.Models;

    public class OutrightViewModel
    {
        private readonly OutrightQuery query;

        public OutrightViewModel(
            OutrightQuery query,
            IEnumerable<TeamLeague> teamLeagues,
            IEnumerable<Summary> summaries,
            IEnumerable<SelectListItem> leagues)
        {
            this.query = query;
            TeamLeagues = teamLeagues;

            SummaryMenuViewModel = new SummaryMenuViewModel(summaries, query.SportTypeGroup);
            LeagueSelectionViewModel = new SingleSelectionViewModel(leagues, query.LeagueId);
        }

        public int PossType => query.PossType;

        public IEnumerable<TeamLeague> TeamLeagues { get; }

        public SummaryMenuViewModel SummaryMenuViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public FilterBaseViewModel FilterViewModel
            => new FilterBaseViewModel(
                query.StakeCondition,
                query.Stake,
                query.PossType,
                LeagueSelectionViewModel);
    }
}