namespace CN88.TotalBets.Features.Overview.Models
{
    public class OverViewByParlayModel
    {
        public int SportTypeGroup { get; }

        public int SoccerParlayWager { get; }

        public int NonSoccerParlayWager { get; }

        public int MixSportsParlayWager { get; }

        public decimal SoccerParlayStake { get; }

        public decimal NonSoccerParlayStake { get; }

        public decimal MixSportsParlayStake { get; }
    }
}