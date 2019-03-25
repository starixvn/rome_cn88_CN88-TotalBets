namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    using System.Collections.Generic;

    public class FilterViewModel
    {
        public FilterViewModel(
            byte type,
            int sportTypeGroup,
            byte stakeCondition,
            decimal stake,
            bool isShowMarket,
            byte market,
            SingleSelectionViewModel possTypeSelectionViewModel,
            SingleSelectionViewModel leagueSelectionViewModel,
            bool isShowLeague = true)
            : this(type,
                  sportTypeGroup,
                  stakeCondition,
                  stake,
                  possTypeSelectionViewModel,
                  isShowMarket,
                  market,
                  isShowLeague,
                  new Dictionary<string, object>())
        {
            LeagueSelectionViewModel = leagueSelectionViewModel;
        }

        public FilterViewModel(
            byte type,
            int sportTypeGroup,
            byte stakeCondition,
            decimal stake,
            SingleSelectionViewModel possTypeSelectionViewModel,
            bool isShowMarket,
            byte market,
            bool isShowLeague,
            IDictionary<string, object> hiddenParams)
        {
            Type = type;
            SportTypeGroup = sportTypeGroup;
            StakeCondition = stakeCondition;
            Stake = stake;
            PossTypeSelectionViewModel = possTypeSelectionViewModel;
            IsShowMarket = isShowMarket;
            Market = market;
            IsShowLeague = isShowLeague;
            HiddenParams = hiddenParams;
        }

        public byte Type { get; }

        public int SportTypeGroup { get; }

        public byte StakeCondition { get; }

        public decimal Stake { get; }

        public bool IsShowMarket { get; }

        public byte Market { get; }

        public bool IsShowLeague { get; }

        public SingleSelectionViewModel PossTypeSelectionViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public IDictionary<string, object> HiddenParams { get; }
    }
}