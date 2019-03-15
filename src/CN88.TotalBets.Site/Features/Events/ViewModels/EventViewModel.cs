namespace CN88.TotalBets.Site.Features.Events.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AppResources;
    using CN88.TotalBets.Shared.Models;
    using Shared.ViewModels;
    using TotalBets.Features.Events.Models;
    using TotalBets.Features.Summaries.Models;
    using TotalBets.Shared.Constants;

    public class EventViewModel : BaseViewModel
    {
        private const string eventVirtualPath = "Event/Index";

        public EventViewModel(
            EventQuery query,
            string sportTypeGroup,
            MatchLeagueTotal eventModel,
            IEnumerable<SportGroupSummary> summaries,
            IEnumerable<SelectListItem> leagues)
            : base(query, sportTypeGroup, $"{query.AppPath}{eventVirtualPath}", summaries, leagues)
        {
            EventModel = eventModel;
            IsInPlay = eventModel.Type == EventTypeConstants.InPlay;
            HasEventData = eventModel.TotalStake == 0;
            SubTitle = Resources.ResourceManager.GetString(EventTypeConstants.EventTypes[query.Type]);
            Query = query;
        }

        public EventQuery Query { get; }

        public MatchLeagueTotal EventModel { get; }

        public bool IsInPlay { get; }

        public bool HasEventData { get; }
    }
}