namespace CN88.TotalBets.Features.SportTypeGroup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Caching;
    using Models;
    using Shared.Services;

    public interface SportTypeGroupService
    {
        Task<IEnumerable<SportGroup>> GetSportTypeGroups();

        Task<string> GetSportTypeGroupName(int sportTypeGroup);
    }

    public class SportTypeGroupServiceImpl : SportTypeGroupService
    {
        private const string SportGroupsCacheKey = "CN88.TotalBets.ServicesCN88.TotalBets.Services.SportGroupServiceImpl.SportGroups";

        private readonly SportTypeGroupDataAccess sportGroupDataAccess;
        private readonly CacheRefService cacheRefService;

        public SportTypeGroupServiceImpl(
            SportTypeGroupDataAccess sportGroupDataAccess,
            CacheRefService cacheRefService)
        {
            this.sportGroupDataAccess = sportGroupDataAccess;
            this.cacheRefService = cacheRefService;
        }

        public async Task<IEnumerable<SportGroup>> GetSportTypeGroups()
            => await ConfiguredCacheService.GetOrSetAsync(
                SportGroupsCacheKey,
                () =>
                {
                    var sportGroups = sportGroupDataAccess.GetSportGroups(new GetSportGroupsCriteria());

                    return sportGroups.Select(group => new SportGroup(group.Id, group.DisplaySportIds, cacheRefService.GetSportName));
                },
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));

        public async Task<string> GetSportTypeGroupName(int sportTypeGroup)
            => (await GetSportTypeGroups()).FirstOrDefault(g => g.Id == sportTypeGroup)?.DisplayName;
    }
}