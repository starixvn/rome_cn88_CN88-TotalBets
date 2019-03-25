namespace CN88.TotalBets.Site.Features.Started.ViewModels
{
    using Shared.ViewModels.Filters;

    public class StartedFilterViewModel : FilterViewModel
    {
        public StartedFilterViewModel(
             byte type,
            int sportTypeGroup,
            byte stakeCondition,
            decimal stake,
            byte market,
            SingleSelectionViewModel possTypeSelectionViewModel,
            SingleSelectionViewModel leagueSelectionViewModel)
            : base(type, sportTypeGroup, stakeCondition, stake, true, market, possTypeSelectionViewModel, leagueSelectionViewModel)
        {
        }
    }
}