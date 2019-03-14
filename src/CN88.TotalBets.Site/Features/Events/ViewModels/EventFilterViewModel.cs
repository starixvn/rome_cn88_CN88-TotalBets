namespace CN88.TotalBets.Site.Features.Events.ViewModels
{
    using Shared.ViewModels.Filters;

    public class EventFilterViewModel : FilterBaseViewModel
    {
        public EventFilterViewModel(
            byte market,
            int sportTypeGroup,
            byte stakeCondition,
            decimal stake,
            byte possType,
            SingleSelectionViewModel leagueSelectionViewModel)
            : base(sportTypeGroup, stakeCondition, stake, possType, leagueSelectionViewModel)
        {
            Market = market;
        }

        public byte Market { get; }
    }
}