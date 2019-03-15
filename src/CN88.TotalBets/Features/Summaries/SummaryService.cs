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
        Task<IEnumerable<SummaryByEvent>> GetSummaryByEventType(int custId, UserLevel custLevel, byte eventType);

        Task<IEnumerable<SummaryBySportTypeGroup>> GetSummaryBySportTypeGroup(int custId, UserLevel custLevel, byte eventType, byte sportTypeGroup);
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

        public async Task<IEnumerable<SummaryByEvent>> GetSummaryByEventType(int custId, UserLevel custLevel, byte eventType)
        {
            var summaries = await dynamicRepository.FetchAsync<SummaryByEvent>(new GetSummaryByEventCriteria(custId, eventType, custLevel));

            if (summaries == null || !summaries.Any())
            {
                return Enumerable.Empty<SummaryByEvent>();
            }

            foreach (var summary in summaries)
            {
                summary.SportTypeGroupName = await sportTypeGroupService.GetSportTypeGroupName(summary.SportTypeGroup);
            }

            return summaries;
        }

        public async Task<IEnumerable<SummaryBySportTypeGroup>> GetSummaryBySportTypeGroup(int custId, UserLevel custLevel, byte eventType, byte sportTypeGroup)
        {
            var summaries = await dynamicRepository.FetchAsync<SummaryBySportTypeGroup>(new GetSummaryBySportTypeGroupCriteria(custId, eventType, sportTypeGroup, custLevel));

            if (summaries == null || !summaries.Any())
            {
                return Enumerable.Empty<SummaryBySportTypeGroup>();
            }

            foreach (var summary in summaries)
            {
                summary.SportTypeGroupName = await sportTypeGroupService.GetSportTypeGroupName(summary.SportTypeGroup);
                summary.LeagueName = cacheRefService.GetLeagueName(summary.LeagueId);
                summary.HomeTeamName = cacheRefService.GetTeamName(summary.HomeId);
                summary.AwayTeamName = cacheRefService.GetTeamName(summary.AwayId);
            }

            return summaries;
        }
    }
}