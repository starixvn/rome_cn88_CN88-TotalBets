namespace CN88.TotalBets.Site.Features.Events.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AppResources;
    using Shared.ViewModels;
    using TotalBets.Features.Events.Models;
    using TotalBets.Features.Summay.Models;
    using TotalBets.Shared.Constants;

    public class EventViewModel : BaseViewModel
    {
        public EventViewModel(
            EventQuery query,
            string sportTypeGroup,
            MatchLeagueTotal eventModel,
            IEnumerable<Summary> summaries,
            IEnumerable<SelectListItem> leagues)
            : base(query, sportTypeGroup, summaries, leagues)
        {
            EventModel = eventModel;
            IsInPlay = eventModel.Type == EventTypeConstants.InPlay;
            HasEventData = eventModel.TotalStake == 0;
            SubTitle = Resources.ResourceManager.GetString(EventTypeConstants.EventTypes[query.Type]);
        }

        public MatchLeagueTotal EventModel { get; }

        public bool IsInPlay { get; }

        public bool HasEventData { get; }
    }
}