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
        public OutrightViewModel(
            OutrightQuery query,
            string sportGroupName,
            IEnumerable<TeamLeague> teamLeagues,
            IEnumerable<Summary> summaries,
            IEnumerable<SelectListItem> leagues)
            : base(query, sportGroupName, summaries, leagues)
        {
            TeamLeagues = teamLeagues;
            SubTitle = Resources.Outright;

            WagerAccountLink = $"WagerAccount/Index?CustId={query.ViewerId}&CustLevelId={query.CustLevelId}&Stake={query.Stake}&StakeCondition={query.StakeCondition}&MatchId=";
        }

        public string SportGroupName { get; }

        public IEnumerable<TeamLeague> TeamLeagues { get; }

        public string WagerAccountLink { get; }
    }
}