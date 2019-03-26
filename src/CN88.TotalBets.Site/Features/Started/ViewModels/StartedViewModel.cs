namespace CN88.TotalBets.Site.Features.Started.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Shared.ViewModels;
    using TotalBets.Features.Started.Models;
    using TotalBets.Features.Summaries.Models;

    public class StartedViewModel : BaseViewModel
    {
        private const string startedVirtualPath = "Started/Index";

        public StartedViewModel(
            StartedQuery query,
            string sportTypeGroup,
            IEnumerable<StartedLeague> data,
            IEnumerable<SportGroupSummary> summaries,
            IEnumerable<SelectListItem> leagues)
            : base(query, sportTypeGroup, $"{query.AppPath}{startedVirtualPath}", summaries, leagues)
        {
            ShowDate = query.DateType == 2;
            Data = data;
            FilterViewModel = new StartedFilterViewModel(
                query.Type,
                query.DateType,
                query.SportTypeGroup,
                query.StakeCondition,
                query.Stake,
                query.Market,
                PossTypeSelectionViewModel,
                LeagueSelectionViewModel);
        }

        public bool ShowDate { get; }

        public IEnumerable<StartedLeague> Data { get; }

        public int TotalPMWager => Data.Sum(item => item.TotalPMWager);

        public decimal TotalPMStake => Data.Sum(item => item.TotalPMStake);

        public int TotalIPWager => Data.Sum(item => item.TotalIPWager);

        public decimal TotalIPStake => Data.Sum(item => item.TotalIPStake);

        public int TotalWager => TotalPMWager + TotalIPWager;

        public decimal TotalStake => TotalPMStake + TotalIPStake;
    }
}