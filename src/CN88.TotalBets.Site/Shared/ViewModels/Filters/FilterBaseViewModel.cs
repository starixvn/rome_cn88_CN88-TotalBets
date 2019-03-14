namespace CN88.TotalBets.Site.Shared.ViewModels.Filters
{
    public class FilterBaseViewModel
    {
        public FilterBaseViewModel(
            int sportTypeGroup,
            byte stakeCondition,
            decimal stake,
            byte possType,
            SingleSelectionViewModel leagueSelectionViewModel)
        {
            SportTypeGroup = sportTypeGroup;
            StakeCondition = stakeCondition;
            Stake = stake;
            PossType = possType;
            LeagueSelectionViewModel = leagueSelectionViewModel;
        }

        public int SportTypeGroup { get; }

        public byte StakeCondition { get; }

        public decimal Stake { get; }

        public byte PossType { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }
    }
}