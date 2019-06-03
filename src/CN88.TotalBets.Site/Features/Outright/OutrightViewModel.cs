namespace CN88.TotalBets.Site.Features.Outright
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AppResources;
    using Shared.ViewModels;
    using TotalBets.Features.Outright.Models;
    using TotalBets.Features.Summaries.Models;
    using Web.Helpers;

    public class OutrightViewModel : TotalBetBySportBaseViewModel
    {
        private const string outrightVirtualPath = "Outright/Index";

        public OutrightViewModel(
            OutrightQuery query,
            string sportGroupName,
            IEnumerable<TeamLeague> teamLeagues,
            IEnumerable<SportGroupSummary> summaries,
            IEnumerable<SelectListItem> leagues,
            BrowserHelper browserHelper)
            : base(query, sportGroupName, $"{query.AppPath}{outrightVirtualPath}", new Dictionary<string, object>(), summaries, leagues, browserHelper)
        {
            TeamLeagues = teamLeagues;
            SubTitle = Resources.Outright;
        }

        public string SportGroupName { get; }

        public IEnumerable<TeamLeague> TeamLeagues { get; }

        public object GetWagerAccountParams(int matchId)
            => new
            {
                custId = query.ViewerId,
                custLevelId = query.ViewerLevelId,
                custName = query.ViewerName,
                matchId,
                FilterViewModel.Stake,
                FilterViewModel.StakeCondition
            };
    }
}