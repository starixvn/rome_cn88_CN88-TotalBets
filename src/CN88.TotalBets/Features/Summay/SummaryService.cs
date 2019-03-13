namespace CN88.TotalBets.Features.Summay
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Shared.Models;
    using SportTypeGroup;

    public interface SummaryService
    {
        Task<IEnumerable<Summary>> GetSummaries(int custId, UserLevel custLevel, byte type);
    }

    public class SummaryServiceImpl : SummaryService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly SportTypeGroupService sportTypeGroupService;

        public SummaryServiceImpl(IDynamicRepository dynamicRepository, SportTypeGroupService sportTypeGroupService)
        {
            this.dynamicRepository = dynamicRepository;
            this.sportTypeGroupService = sportTypeGroupService;
        }

        public async Task<IEnumerable<Summary>> GetSummaries(int custId, UserLevel custLevel, byte type)
        {
            var summaries = await dynamicRepository.FetchAsync<Summary>(new GetSummaryCriteria(custId, type, custLevel));

            if (summaries != null && summaries.Any())
            {
                foreach (var summary in summaries)
                {
                    summary.SportTypeGroupName = await sportTypeGroupService.GetSportTypeGroupName(summary.SportTypeGroup);
                }
            }

            return summaries;
        }
    }
}