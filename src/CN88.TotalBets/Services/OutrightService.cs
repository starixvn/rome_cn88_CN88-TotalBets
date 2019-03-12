namespace CN88.TotalBets.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models.Outright;
    using Params;

    public interface IOutrightService
    {
        Task<IEnumerable<OutrightItem>> GetOutrightsAsync(GetOutrightParam param);
    }

    public class OutrightService : IOutrightService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ICacheRefService cacheRefService;

        public OutrightService(
            IDynamicRepository dynamicRepository,
            ICacheRefService cacheRefService)
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
