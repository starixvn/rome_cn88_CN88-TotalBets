namespace CN88.TotalBets.Site.Features.Outright
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AppResources;
    using Shared.ViewModels;
    using TotalBets.Features.Outright.Models;
    using TotalBets.Features.Summay.Models;

    public class OutrightViewModel : BaseViewModel
    {
        private const string outrightVirtualPath = "Outright/Index";

        public OutrightViewModel(
            OutrightQuery query,
            string sportGroupName,
            IEnumerable<TeamLeague> teamLeagues,
            IEnumerable<Summary> summaries,
            IEnumerable<SelectListItem> leagues)
            : base(query, sportGroupName, $"{query.AppPath}{outrightVirtualPath}", summaries, leagues)
        {
            TeamLeagues = teamLeagues;
            SubTitle = Resources.Outright;
        }

        public string SportGroupName { get; }

        public IEnumerable<TeamLeague> TeamLeagues { get; }
    }
}