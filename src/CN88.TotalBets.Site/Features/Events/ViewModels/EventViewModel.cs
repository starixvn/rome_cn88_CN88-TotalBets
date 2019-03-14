namespace CN88.TotalBets.Site.Features.Events.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Request;
    using Shared.ViewModels.Filters;
    using TotalBets.Features.Events.Models;
    using TotalBets.Features.Summay.Models;

    public class EventViewModel
    {
        private readonly EventQuery query;

        public EventViewModel(
            EventQuery query,
            MatchLeagueTotal eventModel,
            IEnumerable<Summary> summaries,
            IEnumerable<SelectListItem> leagues)
        {
            this.query = query;
            EventModel = eventModel;

            SummaryMenuViewModel = new SummaryMenuViewModel(summaries, query.SportTypeGroup);
            LeagueSelectionViewModel = new SingleSelectionViewModel(leagues, query.LeagueId);
        }

        public MatchLeagueTotal EventModel { get; }

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