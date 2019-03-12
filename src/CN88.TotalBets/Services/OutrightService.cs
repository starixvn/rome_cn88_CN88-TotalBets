namespace CN88.TotalBets.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models.Outright;
    using Params;

    public interface OutrightService
    {
        Task<IEnumerable<OutrightLeague>> GetOutrightsAsync(GetOutrightParam param);
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

        public async Task<IEnumerable<OutrightLeague>> GetOutrightsAsync(GetOutrightParam param)
        {
            var outrights = await dynamicRepository.FetchAsync<OutrightItem>(ToGetOutrightCriteria(param));

            return outrights.Any()
                ? GetOutrightLeagues(outrights)
                : Enumerable.Empty<OutrightLeague>();
        }

        private IEnumerable<OutrightLeague> GetOutrightLeagues(IEnumerable<OutrightItem> outrights)
        {
            IList<OutrightLeague> outrightLeagues = new List<OutrightLeague>();
            var groupLeagues = outrights.GroupBy(item => item.LeagueId);

            foreach (var group in groupLeagues)
            {
                foreach (var item in group)
                {
                    item.TeamName = cacheRefService.GetTeamName(item.TeamId);
                }

                outrightLeagues.Add(new OutrightLeague(group.Key, cacheRefService.GetLeagueName(group.Key), group));
            }

            return outrightLeagues;
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
