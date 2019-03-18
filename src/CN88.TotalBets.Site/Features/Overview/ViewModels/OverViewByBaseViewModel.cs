namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
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

        public OverViewByBaseViewModel(IEnumerable<OverViewBySportTypeModel> model)
        {
            SoccerRow = model.FirstOrDefault(i => i.SportTypeGroup == 1);
            BasketballRow = model.FirstOrDefault(i => i.SportTypeGroup == 2);
            TennisRow = model.FirstOrDefault(i => i.SportTypeGroup == 3);
            VolleyballRow = model.FirstOrDefault(i => i.SportTypeGroup == 4);
            TableTennisRow = model.FirstOrDefault(i => i.SportTypeGroup == 7);
            BadmintonRow = model.FirstOrDefault(i => i.SportTypeGroup == 6);
            BaseballRow = model.FirstOrDefault(i => i.SportTypeGroup == 5);
            SnookerRow = model.FirstOrDefault(i => i.SportTypeGroup == 8);
            OtherSportsRow = model.FirstOrDefault(i => i.SportTypeGroup == 99);
        }

        public abstract string Text { get; }

        public abstract string SubMenuId { get; }

        public abstract string Url(int possType, int sportTypeGroup);

        public OverViewItemModel Soccer { get; protected set; }

        public OverViewItemModel Basketball { get; protected set; }

        public OverViewItemModel Tennis { get; protected set; }

        public OverViewItemModel Volleyball { get; protected set; }

        public OverViewItemModel TableTennis { get; protected set; }

        public OverViewItemModel Badminton { get; protected set; }

        public OverViewItemModel Baseball { get; protected set; }

        public OverViewItemModel Snooker { get; protected set; }

        public OverViewItemModel OtherSports { get; protected set; }


        protected OverViewBySportTypeModel SoccerRow { get; set; }

        protected OverViewBySportTypeModel BasketballRow { get; set; }

        protected OverViewBySportTypeModel TennisRow { get; set; }

        protected OverViewBySportTypeModel VolleyballRow { get; set; }

        protected OverViewBySportTypeModel TableTennisRow { get; set; }

        protected OverViewBySportTypeModel BadmintonRow { get; set; }

        protected OverViewBySportTypeModel BaseballRow { get; set; }

        protected OverViewBySportTypeModel SnookerRow { get; set; }

        protected OverViewBySportTypeModel OtherSportsRow { get; set; }

        public OverViewItemModel Total
            => Soccer + Basketball + Tennis + Volleyball + TableTennis + Badminton + Baseball + Snooker + OtherSports;
    }
}