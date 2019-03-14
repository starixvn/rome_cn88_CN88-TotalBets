namespace CN88.TotalBets.Site.Features.Events.ViewModels
{
    using Shared.ViewModels.Filters;

    public class EventFilterViewModel : FilterBaseViewModel
    {
        public EventFilterViewModel(
            byte type,
            byte market,
            int sportTypeGroup,
            byte stakeCondition,
            decimal stake,
            byte possType,
            SingleSelectionViewModel leagueSelectionViewModel)
            : base(sportTypeGroup, stakeCondition, stake, possType, leagueSelectionViewModel)
        {
            Type = type;
            Market = market;
        }

        public byte Market { get; }

        public byte Type { get; }
    }
}