namespace CN88.TotalBets.Models.OverView
{
    public class OverViewByParlayModel
    {
        public OverViewByParlayModel()
        {
        }

        public int SoccerParlayWager { get; }

        public int NonSoccerParlayWager { get; }

        public int MixSportsParlayWager { get; }

        public decimal SoccerParlayStake { get; }

        public decimal NonSoccerParlayStake { get; }

        public decimal MixSportsParlayStake { get; }
    }
}
