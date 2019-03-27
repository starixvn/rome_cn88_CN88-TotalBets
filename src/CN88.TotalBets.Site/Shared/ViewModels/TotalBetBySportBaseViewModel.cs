namespace CN88.TotalBets.Site.Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Filters;
    using Requests;
    using TotalBets.Features.Summaries.Models;

    public class TotalBetBySportBaseViewModel : BaseViewModel
    {
        public TotalBetBySportBaseViewModel(
            FilterQuery query,
            string title,
            string appPath,
            IDictionary<string, object> hiddenParams,
            IEnumerable<SportGroupSummary> summaries,
            IEnumerable<SelectListItem> leagues,
            bool isShowDateSelection = false,
            bool isShowMarketSelection = false)
            : base(query, title, hiddenParams, leagues, isShowDateSelection, isShowMarketSelection, true)
        {
            SummaryMenuViewModel = new SummaryMenuViewModel(summaries, query.SportTypeGroup, appPath);
        }

        public SummaryMenuViewModel SummaryMenuViewModel { get; }
    }
}