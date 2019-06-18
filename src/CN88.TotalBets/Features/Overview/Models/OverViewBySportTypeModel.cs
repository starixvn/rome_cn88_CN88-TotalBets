namespace CN88.TotalBets.Features.Overview.Models
{
    public class OverViewBySportTypeModel
    {
        public OverViewBySportTypeModel()
        {
        }

        public OverViewBySportTypeModel(
            int sportTypeGroup,
            int inplayWager,
            int todayWager,
            int earlyWager,
            int outrightWager,
            decimal inplayStake,
            decimal todayStake,
            decimal earlyStake,
            decimal outrightStake)
        {
            SportTypeGroup = sportTypeGroup;
            InplayWager = inplayWager;
            TodayWager = todayWager;
            EarlyWager = earlyWager;
            OutrightWager = outrightWager;
            InplayStake = inplayStake;
            TodayStake = todayStake;
            EarlyStake = earlyStake;
            OutrightStake = outrightStake;
        }

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