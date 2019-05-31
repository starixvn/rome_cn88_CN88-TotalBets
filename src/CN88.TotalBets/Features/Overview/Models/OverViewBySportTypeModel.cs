namespace CN88.TotalBets.Features.Overview.Models
{
    public class OverViewBySportTypeModel
    {
        public int SportTypeGroup { get; }

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