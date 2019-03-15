namespace CN88.TotalBets.Site.Features.Outright
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AppResources;
    using Shared.ViewModels;
    using TotalBets.Features.Outright.Models;
    using TotalBets.Features.Summaries.Models;

    public class OutrightViewModel : BaseViewModel
    {
        private const string outrightVirtualPath = "Outright/Index";

        public OutrightViewModel(
            OutrightQuery query,
            string sportGroupName,
            IEnumerable<TeamLeague> teamLeagues,
            IEnumerable<SummaryByEvent> summaries,
            IEnumerable<SelectListItem> leagues)
            : base(query, sportGroupName, $"{query.AppPath}{outrightVirtualPath}", summaries, leagues)
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
                matchId = matchId,
                Stake = FilterViewModel.Stake,
                StakeCondition = FilterViewModel.StakeCondition
            };
    }
}