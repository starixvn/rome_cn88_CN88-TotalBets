namespace CN88.TotalBets.Models.OverView
{
    public class OverViewBySportTypeModel
    {
        public OverViewBySportTypeModel()
        {
        }

        public int SportType { get; }

        public int InplayWager { get; }

        public int TodayWager { get; }

        public int EarlyWager { get; }

        public int OutrightWager { get; }

        public decimal InplayStake { get; }

        public decimal TodayStake { get; }

        public decimal EarlyStake { get; }

        public decimal OutrightStake { get; }
    }
}
