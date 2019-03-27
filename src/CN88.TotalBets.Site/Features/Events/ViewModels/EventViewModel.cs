namespace CN88.TotalBets.Site.Features.Events.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using Shared.ViewModels;
    using TotalBets.Features.Events.Models;
    using TotalBets.Features.Summaries.Models;
    using TotalBets.Shared.Constants;

    public class EventViewModel : TotalBetBySportBaseViewModel
    {
        private const string eventVirtualPath = "Event/Index";

        public EventViewModel(
            EventQuery query,
            string sportTypeGroup,
            MatchLeagueTotal eventModel,
            IEnumerable<SportGroupSummary> summaries,
            IEnumerable<SelectListItem> leagues) : base(
                query,
                sportTypeGroup,
                $"{query.AppPath}{eventVirtualPath}?Type={query.Type}",
                new Dictionary<string, object> { [nameof(query.Type)] = query.Type },
                summaries,
                leagues,
                false,
                query.Type == EventTypeConstants.InPlay)
        {
            EventModel = eventModel;
            IsInPlay = eventModel.Type == EventTypeConstants.InPlay;
            IsEarly = eventModel.Type == EventTypeConstants.Early;
            HasEventData = eventModel.MatchLeagues.Any();
            SubTitle = Resources.ResourceManager.GetString(EventTypeConstants.EventTypes[query.Type]);
            Query = query;
        }

        public EventQuery Query { get; }

        public MatchLeagueTotal EventModel { get; }

        public bool IsInPlay { get; }

        public bool IsEarly { get; }

        public bool HasEventData { get; }

        public bool IsShowScore
            => Query.SportTypeGroup == SportTypeConstants.Soccer;
    }
}