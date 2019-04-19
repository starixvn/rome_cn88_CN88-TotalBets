namespace CN88.TotalBets.Site.Features.Started.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using Shared.ViewModels;
    using TotalBets.Features.Started.Models;
    using TotalBets.Features.Summaries.Models;
    using TotalBets.Shared.Constants;

    public class StartedViewModel : TotalBetBySportBaseViewModel
    {
        private const string startedVirtualPath = "Started/Index";

        public StartedViewModel(
            StartedQuery query,
            string sportTypeGroup,
            IEnumerable<StartedLeague> data,
            IEnumerable<SportGroupSummary> summaries,
            IEnumerable<SelectListItem> leagues)
            : base(
                  query,
                  sportTypeGroup,
                  $"{query.AppPath}{startedVirtualPath}",
                  new Dictionary<string, object>
                  {
                      ["IsShowMarketSelection"] = true
                  },
                  summaries,
                  leagues,
                  isShowDateSelection: true,
                  isShowMarketSelection: true)
        {
            SubTitle = Resources.Started;
            Data = data;
        }

        public bool IsAutoRefresh { get; }

        public IEnumerable<StartedLeague> Data { get; }

        public int TotalPMWager => Data.Sum(item => item.TotalPMWager);

        public decimal TotalPMStake => Data.Sum(item => item.TotalPMStake);

        public int TotalIPWager => Data.Sum(item => item.TotalIPWager);

        public decimal TotalIPStake => Data.Sum(item => item.TotalIPStake);

        public int TotalWager => TotalPMWager + TotalIPWager;

        public decimal TotalStake => TotalPMStake + TotalIPStake;

        protected override IDictionary<string, object> GetViewByBetTypeRoutesValues(byte type, int matchId)
            => new Dictionary<string, object>(base.GetViewByBetTypeRoutesValues(type, matchId))
            {
                [nameof(query.Market)] = query.Market,
                [nameof(query.DateType)] = query.DateType,
                ["IsShowKODate"] = true
            };
    }
}