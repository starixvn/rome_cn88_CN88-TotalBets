namespace CN88.TotalBets.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models;

    public interface LeagueService
    {
        Task<IEnumerable<League>> GetLeagues(int sportTypeGroup, int type);
    }

    public class LeagueServiceImpl : LeagueService
    {
        private const string LeagueCacheKey = "CN88.TotalBets.Services.LeagueServiceImpl.Leagues";
        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;

        public LeagueServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService)
        {
            this.dynamicRepository = dynamicRepository;
            this.cacheRefService = cacheRefService;
        }

        public async Task<IEnumerable<League>> GetLeagues(int sportTypeGroup, int type)
            => await ConfiguredCacheService.GetOrSetAsync(
                $"{LeagueCacheKey}.{type}.{sportTypeGroup}",
                () =>
                {
                    var leagues = dynamicRepository.Fetch<League>(new GetLeagueCriteria(sportTypeGroup, type));
                    return leagues.Select(league => new League(league.Id, cacheRefService.GetLeagueName(league.Id)));
                },
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
    }
}