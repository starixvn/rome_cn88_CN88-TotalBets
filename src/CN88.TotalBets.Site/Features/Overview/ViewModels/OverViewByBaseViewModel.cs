namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using TotalBets.Features.Overview.Models;

    public abstract class OverViewByBaseViewModel
    {
        public OverViewByBaseViewModel()
        {
            Soccer = new OverViewItemModel();
            Basketball = new OverViewItemModel();
            Tennis = new OverViewItemModel();
            Volleyball = new OverViewItemModel();
            TableTennis = new OverViewItemModel();
            Badminton = new OverViewItemModel();
            Baseball = new OverViewItemModel();
            Snooker = new OverViewItemModel();
            OtherSports = new OverViewItemModel();
        }

        public abstract string Text { get; }

        public OverViewItemModel Soccer { get; protected set; }

        public OverViewItemModel Basketball { get; protected set; }

        public OverViewItemModel Tennis { get; protected set; }

        public OverViewItemModel Volleyball { get; protected set; }

        public OverViewItemModel TableTennis { get; protected set; }

        public OverViewItemModel Badminton { get; protected set; }

        public OverViewItemModel Baseball { get; protected set; }

        public OverViewItemModel Snooker { get; protected set; }

        public OverViewItemModel OtherSports { get; protected set; }

        public OverViewItemModel Total
            => Soccer + Basketball + Tennis + Volleyball + TableTennis + Badminton + Baseball + Snooker + OtherSports;
    }
}