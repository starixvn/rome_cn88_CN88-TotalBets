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
            SingleSelectionViewModel leagueSelectionViewModel)
        {
            Type = type;
            SportTypeGroup = sportTypeGroup;
            StakeCondition = stakeCondition;
            Stake = stake;
            PossType = possType;
            IsShowMarket = isShowMarket;
            Market = market;
            LeagueSelectionViewModel = leagueSelectionViewModel;
        }

        public byte Type { get; }

        public int SportTypeGroup { get; }

        public byte StakeCondition { get; }

        public decimal Stake { get; }

        public byte PossType { get; }

        public bool IsShowMarket { get; }

        public byte Market { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }
    }
}