namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    using System.Collections.Generic;

    public class FilterViewModel
    {
        public FilterViewModel(
            int sportTypeGroup,
            byte stakeCondition,
            decimal stake,
            IDictionary<string, object> hiddenParams,
            SingleSelectionViewModel possTypeSelectionViewModel,
            DateSelectionViewModel dateSelectionViewModel = null,
            MarketSelectionViewModel marketSelectionViewModel = null,
            SingleSelectionViewModel leagueSelectionViewModel = null)
        {
            SportTypeGroup = sportTypeGroup;
            StakeCondition = stakeCondition;
            Stake = stake;
            HiddenParams = hiddenParams;

            PossTypeSelectionViewModel = possTypeSelectionViewModel;

            IsShowDateSelection = dateSelectionViewModel != null;
            DateSelectionViewModel = dateSelectionViewModel;

            IsShowMarketSelection = marketSelectionViewModel != null;
            MarketSelectionViewModel = marketSelectionViewModel;

            IsShowLeagueSelection = leagueSelectionViewModel != null;
            LeagueSelectionViewModel = leagueSelectionViewModel;
        }

        public int SportTypeGroup { get; }

        public byte StakeCondition { get; }

        public decimal Stake { get; }

        public bool IsShowMarketSelection { get; }

        public byte Market { get; }

        public bool IsShowDateSelection { get; }

        public bool IsShowLeagueSelection { get; }

        public DateSelectionViewModel DateSelectionViewModel { get; }

        public SingleSelectionViewModel PossTypeSelectionViewModel { get; }

        public MarketSelectionViewModel MarketSelectionViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public IDictionary<string, object> HiddenParams { get; }
    }
}