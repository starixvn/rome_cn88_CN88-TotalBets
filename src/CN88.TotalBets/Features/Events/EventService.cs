namespace CN88.TotalBets.Features.Events
{
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using Shared.Services;

    public interface EventService
    {
        Task<MatchLeagueTotal> GetEventsAsync(EventParam param);

        Task<Match> GetMatch(int matchId);
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

        public async Task<MatchLeagueTotal> GetEventsAsync(EventParam param)
        {
            var matches = await dynamicRepository.FetchAsync<Match>(new GetEventCriteria(param));

            int totalWager = 0;
            decimal totalStake = 0;

            var matchLeagues = matches.Any()
                ? matches
                    .GroupBy(item => item.LeagueId)
                    .Select(group => GetMatchLeague(group, ref totalWager, ref totalStake))
                    .OrderBy(item => item.LeagueName)
                    .ToList()
                : Enumerable.Empty<MatchLeague>();

            return new MatchLeagueTotal(param.Type, matchLeagues, totalWager, totalStake);
        }

        private MatchLeague GetMatchLeague(
            IGrouping<int, Match> groupLeague,
            ref int totalWager,
            ref decimal totalStake)
        {
            var totalWagerInLeague = 0;
            var totalStakeInLeague = 0M;

            foreach (var item in groupLeague)
            {
                totalWagerInLeague += item.Wager;
                totalStakeInLeague += item.Stake;
            }

            totalWager += totalWagerInLeague;
            totalStake += totalStakeInLeague;

            var orderedGroup = groupLeague.OrderByDescending(item => item.Stake);

            return new MatchLeague(
                groupLeague.Key,
                cacheRefService.GetLeagueName(groupLeague.Key),
                orderedGroup,
                totalWagerInLeague,
                totalStakeInLeague);
        }

        public async Task<Match> GetMatch(int matchId)
            => await dynamicRepository.GetAsync<Match>(new GetEventInfoCriteria(matchId));
    }
}