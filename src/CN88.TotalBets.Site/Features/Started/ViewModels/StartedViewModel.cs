namespace CN88.TotalBets.Site.Features.Started.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Shared.ViewModels;
    using TotalBets.Features.Events.Models;
    using TotalBets.Features.Summaries.Models;

    public class StartedViewModel : BaseViewModel
    {
        private const string startedVirtualPath = "Started/Index";

        public StartedViewModel(
            StartedQuery query,
            string sportTypeGroup,
            IEnumerable<MatchLeague> data,
            IEnumerable<SportGroupSummary> summaries,
            IEnumerable<SelectListItem> leagues)
            : base(query, sportTypeGroup, $"{query.AppPath}{startedVirtualPath}", summaries, leagues)
        {
            Data = data;
        }

        public IEnumerable<MatchLeague> Data { get; }
    }
}