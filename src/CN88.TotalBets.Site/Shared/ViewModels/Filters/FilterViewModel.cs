using System.Collections.Generic;

namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    public class FilterViewModel
    {
        public FilterViewModel(
            byte type,
            int sportTypeGroup,
            byte stakeCondition,
            decimal stake,
            byte possType,
            bool isShowMarket,
            byte market,
            SingleSelectionViewModel leagueSelectionViewModel,
            bool isShowLeague = true)
            : this(type, 
                  sportTypeGroup, 
                  stakeCondition, 
                  stake, 
                  possType, 
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
            byte possType,
            bool isShowMarket,
            byte market,
            bool isShowLeague,
            IDictionary<string, object> hiddenParams)
        {
            Type = type;
            SportTypeGroup = sportTypeGroup;
            StakeCondition = stakeCondition;
            Stake = stake;
            PossType = possType;
            IsShowMarket = isShowMarket;
            Market = market;
            IsShowLeague = isShowLeague;
            HiddenParams = hiddenParams;
        }

        public byte Type { get; }

        public int SportTypeGroup { get; }

        public byte StakeCondition { get; }

        public decimal Stake { get; }

        public byte PossType { get; }

        public bool IsShowMarket { get; }

        public byte Market { get; }

        public bool IsShowLeague { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public IDictionary<string, object> HiddenParams { get; }
    }
}