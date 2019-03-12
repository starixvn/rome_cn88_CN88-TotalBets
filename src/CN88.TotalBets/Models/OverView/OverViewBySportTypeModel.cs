using CN88.TotalBets.Services;

namespace CN88.TotalBets.Models.OverView
{
    public class OverViewBySportTypeModel
    {
        private readonly CacheRefService cacheRefService;
        public OverViewBySportTypeModel()
        {
        }

        public OverViewBySportTypeModel(CacheRefService cacheRefService)
        {
            this.cacheRefService = cacheRefService;
        }

        public int SportTypeGroup { get; }

        public string SportName => cacheRefService.GetSportName(SportTypeGroup);

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
