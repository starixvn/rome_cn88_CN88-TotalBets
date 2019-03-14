namespace CN88.TotalBets.Site.Features.Events.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Request;
    using Shared.ViewModels.Filters;
    using TotalBets.Features.Events.Models;
    using TotalBets.Features.Summay.Models;

    public class InPlayViewModel
    {
        private readonly InPlayQuery query;

        public InPlayViewModel(
            InPlayQuery query,
            IEnumerable<MatchLeague> matchLeagues,
            IEnumerable<Summary> summaries,
            IEnumerable<SelectListItem> leagues)
        {
            this.query = query;
            MatchLeagues = matchLeagues;

            SummaryMenuViewModel = new SummaryMenuViewModel(summaries, query.SportTypeGroup);
            LeagueSelectionViewModel = new SingleSelectionViewModel(leagues, query.LeagueId);
        }

        public IEnumerable<MatchLeague> MatchLeagues { get; }

        public SummaryMenuViewModel SummaryMenuViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public EventFilterViewModel FilterViewModel
            => new EventFilterViewModel(
                query.IsLive,
                query.SportTypeGroup,
                query.StakeCondition,
                query.Stake,
                query.PossType,
                LeagueSelectionViewModel);
    }
}