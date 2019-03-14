namespace CN88.TotalBets.Features.Outright
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using Shared.Services;

    public interface OutrightService
    {
        Task<IEnumerable<OutrightLeague>> GetOutrightsAsync(OutrightParam param);
    }

    public class OutrightServiceImpl : OutrightService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;

        public OutrightServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService)
        {
            this.dynamicRepository = dynamicRepository;
            this.cacheRefService = cacheRefService;
        }

        public async Task<IEnumerable<OutrightLeague>> GetOutrightsAsync(OutrightParam param)
        {
            var outrights = await dynamicRepository.FetchAsync<OutrightItem>(new GetOutrightCriteria(param));

            return outrights.Any()
                ? outrights
                    .GroupBy(item => item.LeagueId)
                    .Select(group => GetOutrightLeague(group))
                    .ToList()
                : Enumerable.Empty<OutrightLeague>();
        }

        private OutrightLeague GetOutrightLeague(IGrouping<int, OutrightItem> groupLeague)
        {
            int totalWager = 0;
            decimal totalStake = 0;

            foreach (var item in groupLeague)
            {
                totalWager += item.Wager;
                totalStake += item.Stake;
                item.TeamName = cacheRefService.GetTeamName(item.TeamId);
            }

            return new OutrightLeague(
                groupLeague.Key,
                cacheRefService.GetLeagueName(groupLeague.Key),
                groupLeague,
                totalWager,
                totalStake);
        }
    }
}