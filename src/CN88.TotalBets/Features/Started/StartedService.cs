namespace CN88.TotalBets.Features.Started
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Events.Models;
    using Fanex.Data.Repository;
    using Params;
    using Shared.Services;

    public interface StartedService
    {
        Task<IEnumerable<MatchLeague>> GetStarted(StartedParam param);
    }

    public class StartedServiceImpl : StartedService
    {
        private readonly CacheRefService cacheRefService;
        private readonly IDynamicRepository dynamicRepository;

        public StartedServiceImpl(
            CacheRefService cacheRefService,
            IDynamicRepository dynamicRepository)
        {
            this.cacheRefService = cacheRefService;
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<MatchLeague>> GetStarted(StartedParam param)
        {
            var matches = await dynamicRepository.FetchAsync<Match>(new GetStartedCriteria(param));

            if (!matches.Any())
            {
                return Enumerable.Empty<MatchLeague>();
            }

            return matches
                .GroupBy(item => item.LeagueId)
                .Select(group => new MatchLeague(
                    group.Key,
                    cacheRefService.GetLeagueName(group.Key),
                    group.OrderByDescending(item => item.Stake),
                    group.Sum(item => item.Wager),
                    group.Sum(item => item.Stake)));
        }
    }
}