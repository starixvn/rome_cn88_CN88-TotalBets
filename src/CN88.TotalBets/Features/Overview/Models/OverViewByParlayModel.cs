namespace CN88.TotalBets.Features.Overview.Models
{
    public class OverViewByParlayModel
    {
        public OverViewByParlayModel()
        {
        }

        public OverViewByParlayModel(
            int sportTypeGroup,
            int soccerParlayWager,
            int nonSoccerParlayWager,
            int mixSportsParlayWager,
            decimal soccerParlayStake,
            decimal nonSoccerParlayStake,
            decimal mixSportsParlayStake)
        {
            SportTypeGroup = sportTypeGroup;
            SoccerParlayWager = soccerParlayWager;
            NonSoccerParlayWager = nonSoccerParlayWager;
            MixSportsParlayWager = mixSportsParlayWager;
            SoccerParlayStake = soccerParlayStake;
            NonSoccerParlayStake = nonSoccerParlayStake;
            MixSportsParlayStake = mixSportsParlayStake;
        }

        public int SportTypeGroup { get; }

        public int SoccerParlayWager { get; }

        public int NonSoccerParlayWager { get; }

        public int MixSportsParlayWager { get; }

        public decimal SoccerParlayStake { get; }

        public decimal NonSoccerParlayStake { get; }

        public decimal MixSportsParlayStake { get; }
    }
}