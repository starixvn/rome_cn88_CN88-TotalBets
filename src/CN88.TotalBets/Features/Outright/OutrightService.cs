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
        Task<IEnumerable<TeamLeague>> GetOutrightsAsync(OutrightParam param);
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

        public async Task<IEnumerable<TeamLeague>> GetOutrightsAsync(OutrightParam param)
        {
            var teams = await dynamicRepository.FetchAsync<Team>(new GetOutrightCriteria(param));

            return teams.Any()
                ? teams
                    .GroupBy(item => item.LeagueId)
                    .Select(group => GetTeamLeague(group))
                    .ToList()
                : Enumerable.Empty<TeamLeague>();
        }

        private TeamLeague GetTeamLeague(IGrouping<int, Team> groupLeague)
        {
            int totalWager = 0;
            decimal totalStake = 0;

            foreach (var item in groupLeague)
            {
                totalWager += item.Wager;
                totalStake += item.Stake;
                item.TeamName = cacheRefService.GetTeamName(item.TeamId);
            }

            return new TeamLeague(
                groupLeague.Key,
                cacheRefService.GetLeagueName(groupLeague.Key),
                groupLeague.OrderByDescending(item => item.Stake),
                totalWager,
                totalStake);
        }
    }
}