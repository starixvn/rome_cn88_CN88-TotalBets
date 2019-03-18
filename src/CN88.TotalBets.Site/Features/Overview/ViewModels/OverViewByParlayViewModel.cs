namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using TotalBets.Features.Overview.Models;

    public class OverViewByParlayViewModel
    {
        public OverViewByParlayViewModel(OverViewByParlayModel model)
        {
            Soccer = new OverViewItemModel(model.SportTypeGroup, model.SoccerParlayWager, model.SoccerParlayStake);
            NonSoccer = new OverViewItemModel(model.SportTypeGroup, model.NonSoccerParlayWager, model.NonSoccerParlayStake);
            MixSports = new OverViewItemModel(model.SportTypeGroup, model.MixSportsParlayWager, model.MixSportsParlayStake);

            Total = Soccer + NonSoccer + MixSports;
        }

        public string Text => "Parlay";

        public string SubMenuId => "PARLAY";

        public OverViewItemModel Soccer { get; }

        public OverViewItemModel NonSoccer { get; }

        public OverViewItemModel MixSports { get; }

        public OverViewItemModel Total { get; }
    }
}