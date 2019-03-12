namespace CN88.TotalBets.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models.Outright;
    using Params;

    public interface OutrightService
    {
        Task<IEnumerable<OutrightItem>> GetOutrightsAsync(GetOutrightParam param);
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

        public async Task<IEnumerable<OutrightItem>> GetOutrightsAsync(GetOutrightParam param)
        {
            var outrights = await dynamicRepository.FetchAsync<OutrightItem>(ToGetOutrightCriteria(param));

            foreach (var item in outrights)
            {
                SetLeagueNameTeamName(item);
            }

            return outrights;
        }

        private void SetLeagueNameTeamName(OutrightItem item)
        {
            item.LeagueName = cacheRefService.GetLeagueName(item.LeagueId);
            item.TeamName = cacheRefService.GetTeamName(item.TeamId);
        }

        private static CriteriaBase ToGetOutrightCriteria(GetOutrightParam param)
            => new GetOutrightCriteria(
                param.CustId,
                param.UserLevel,
                param.SportTypeGroup,
                param.LeagueId,
                param.StakeCondition,
                param.Stake,
                param.PossType);
    }
}
