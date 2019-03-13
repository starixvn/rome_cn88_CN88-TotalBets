namespace CN88.TotalBets.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using DataAccesses;
    using Fanex.Caching;
    using Models;

    public interface SportTypeGroupService
    {
        Task<IEnumerable<SportGroup>> GetSportGroups();
    }

    public class SportTypeGroupServiceImpl : SportTypeGroupService
    {
        private const string SportGroupsCacheKey = "CN88.TotalBets.ServicesCN88.TotalBets.Services.SportGroupServiceImpl.SportGroups";

        private readonly SportTypeGroupDataAccess sportGroupDataAccess;
        private readonly Func<int, string> getSportDisplayName;

        public SportTypeGroupServiceImpl(
            SportTypeGroupDataAccess sportGroupDataAccess,
            Func<int, string> getSportDisplayName)
        {
            this.sportGroupDataAccess = sportGroupDataAccess;
            this.getSportDisplayName = getSportDisplayName;
        }

        public async Task<IEnumerable<SportGroup>> GetSportGroups()
            => await ConfiguredCacheService.GetOrSetAsync(
                SportGroupsCacheKey,
                () =>
                {
                    var sportGroups = sportGroupDataAccess.GetSportGroups(new GetSportGroupsCriteria());

                    return sportGroups.Select(group => new SportGroup(group.Id, group.DisplaySportIds, getSportDisplayName));
                },
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
    }
}