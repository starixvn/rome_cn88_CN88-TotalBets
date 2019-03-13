namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    public class FilterBaseViewModel
    {
        public FilterBaseViewModel(
            byte stakeCondition,
            decimal stake,
            byte possType,
            SingleSelectionViewModel leagueSelectionViewModel)
        {
            StakeCondition = stakeCondition;
            Stake = stake;
            PossType = possType;
            LeagueSelectionViewModel = leagueSelectionViewModel;
        }

        public byte StakeCondition { get; }

        public decimal Stake { get; }

        public byte PossType { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }
    }
}
