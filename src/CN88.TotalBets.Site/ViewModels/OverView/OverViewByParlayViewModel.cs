using CN88.TotalBets.Models.OverView;

namespace CN88.TotalBets.Site.ViewModels
{
    public class OverViewByParlayViewModel
    {
        public OverViewByParlayViewModel(OverViewByParlayModel model)
        {
            Soccer = new OverViewItemModel(model.SoccerParlayWager, model.SoccerParlayStake);
            NonSoccer = new OverViewItemModel(model.NonSoccerParlayWager, model.NonSoccerParlayStake);
            MixSports = new OverViewItemModel(model.MixSportsParlayWager, model.MixSportsParlayStake);

            Total = Soccer + NonSoccer + MixSports;
        }

        public string Text => "Parlay";

        public OverViewItemModel Soccer { get; }

        public OverViewItemModel NonSoccer { get; }

        public OverViewItemModel MixSports { get; }

        public OverViewItemModel Total { get; }
    }
}