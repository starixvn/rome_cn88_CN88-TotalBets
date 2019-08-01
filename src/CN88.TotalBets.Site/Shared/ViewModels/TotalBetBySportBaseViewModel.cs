namespace CN88.TotalBets.Site.Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Filters;
    using Requests;
    using TotalBets.Features.Summaries.Models;
    using TotalBets.Shared.Constants;
    using Web.Helpers;

    public class TotalBetBySportBaseViewModel : BaseViewModel
    {
        private readonly BrowserHelper browserHelper;

        public TotalBetBySportBaseViewModel(
            FilterQuery query,
            string title,
            string appPath,
            IDictionary<string, object> hiddenParams,
            IEnumerable<SportGroupSummary> summaries,
            IEnumerable<SelectListItem> leagues,
            BrowserHelper browserHelper,
            bool isShowDateSelection = false,
            bool isShowMarketSelection = false)
            : base(query, title, hiddenParams, leagues, isShowDateSelection, isShowMarketSelection, true)
        {
            SummaryMenuViewModel = new SummaryMenuViewModel(summaries, query.SportTypeGroup, appPath);
            this.browserHelper = browserHelper;
        }

        public bool IsShowScore => query.SportTypeGroup == SportTypeConstants.Soccer;

        public SummaryMenuViewModel SummaryMenuViewModel { get; }

        public bool IsDevices => browserHelper.IsMobile();

        public string BuildViewByBetTypeLink(byte type, int matchId)
            => Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc(
                Helpers.UrlHelper.MapTotalBetsPath("~/ViewByBetType/Index"),
                GetViewByBetTypeRoutesValues(type, matchId));

        protected virtual IDictionary<string, object> GetViewByBetTypeRoutesValues(byte type, int matchId)
            => new Dictionary<string, object>
            {
                [nameof(type)] = type,
                [nameof(matchId)] = matchId,
                [nameof(query.Stake)] = query.Stake,
                [nameof(query.StakeCondition)] = query.StakeCondition,
                [nameof(query.SportTypeGroup)] = query.SportTypeGroup,
                [nameof(query.PossType)] = query.PossType,
                [nameof(query.LeagueId)] = query.LeagueId,
                [nameof(query.IsAutoRefresh)] = query.IsAutoRefresh
            };
    }
}
