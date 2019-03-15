namespace CN88.TotalBets.Features.Summaries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Shared.Models;
    using Shared.Services;
    using SportTypeGroup;

    public interface SummaryService
    {
        Task<IEnumerable<SportGroupSummary>> GetSportGroupSummaries(int custId, UserLevel custLevel, byte eventType);

        Task<SportGroupSummary> GetSportGroupSummary(int custId, UserLevel custLevel, byte eventType, byte sportTypeGroup);
    }

    public class SummaryServiceImpl : SummaryService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;
        private readonly SportTypeGroupService sportTypeGroupService;

        public SummaryServiceImpl(
            IDynamicRepository dynamicRepository,
            CacheRefService cacheRefService,
            SportTypeGroupService sportTypeGroupService)
        {
            this.dynamicRepository = dynamicRepository;
            this.cacheRefService = cacheRefService;
            this.sportTypeGroupService = sportTypeGroupService;
        }

        public async Task<IEnumerable<SportGroupSummary>> GetSportGroupSummaries(int custId, UserLevel custLevel, byte eventType)
        {
            var summaries = await dynamicRepository.FetchAsync<SportGroupSummary>(new GetSummaryByEventCriteria(custId, eventType, custLevel));

            if (summaries == null || !summaries.Any())
            {
                return Enumerable.Empty<SportGroupSummary>();
            }

            foreach (var summary in summaries)
            {
                summary.SportTypeGroupName = await sportTypeGroupService.GetSportTypeGroupName(summary.SportTypeGroup);
            }

            return summaries;
        }

        public async Task<SportGroupSummary> GetSportGroupSummary(int custId, UserLevel custLevel, byte eventType, byte sportTypeGroup)
        {
            var dtos = await dynamicRepository.FetchAsync<SummaryByMatchDto>(
                new GetSummaryBySportTypeGroupCriteria(custId, eventType, sportTypeGroup, custLevel));

            if (dtos == null || !dtos.Any())
            {
                return null;
            }

            var leagueGroups = dtos.GroupBy(x => x.LeagueId);
            var leagueSummaries = new List<LeagueSummary>();

            foreach (var group in leagueGroups)
            {
                var matchSummaries = group.Select(
                    g => new MatchSummary(
                        g.MatchId,
                        g.HomeId,
                        cacheRefService.GetTeamName(g.HomeId),
                        g.AwayId,
                        cacheRefService.GetTeamName(g.AwayId),
                        g.Wager, g.Stake));

                leagueSummaries.Add(new LeagueSummary(group.Key, cacheRefService.GetLeagueName(group.Key), matchSummaries));
            }

            return new SportGroupSummary(
                sportTypeGroup,
                await sportTypeGroupService.GetSportTypeGroupName(sportTypeGroup),
                leagueSummaries);
        }
    }
}