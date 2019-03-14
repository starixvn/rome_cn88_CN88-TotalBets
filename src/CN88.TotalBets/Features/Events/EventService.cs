namespace CN88.TotalBets.Features.Events
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using Shared.Services;

    public interface EventService
    {
        Task<IEnumerable<MatchLeague>> GetInPlayAsync(InPlayParam param);
    }

    public class EventServiceImpl : EventService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;

        public EventServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService)
        {
            this.dynamicRepository = dynamicRepository;
            this.cacheRefService = cacheRefService;
        }

        public async Task<IEnumerable<MatchLeague>> GetInPlayAsync(InPlayParam param)
        {
            var matches = await dynamicRepository.FetchAsync<Match>(new GetInPlayCriteria(param));

            return matches.Any()
                ? matches
                    .GroupBy(item => item.LeagueId)
                    .Select(group => GetMatchLeague(group))
                    .ToList()
                : Enumerable.Empty<MatchLeague>();
        }

        private MatchLeague GetMatchLeague(IGrouping<int, Match> groupLeague)
        {
            int totalWager = 0;
            decimal totalStake = 0;

            foreach (var item in groupLeague)
            {
                totalWager += item.Wager;
                totalStake += item.Stake;
                item.Event = GetEvent(item.HomeId, item.AwayId);
            }

            return new MatchLeague(
                groupLeague.Key,
                cacheRefService.GetLeagueName(groupLeague.Key),
                groupLeague,
                totalWager,
                totalStake);
        }

        private string GetEvent(int homeId, int awayId)
            => $"{cacheRefService.GetTeamName(homeId)} v {cacheRefService.GetTeamName(awayId)}";
    }
}
