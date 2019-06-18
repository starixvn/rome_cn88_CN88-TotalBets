using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CN88.TotalBets.Features.SportTypeGroup;
using CN88.TotalBets.Features.SportTypeGroup.Criteria;
using CN88.TotalBets.Features.SportTypeGroup.Models;
using CN88.TotalBets.Shared.Services;
using CN88.TotalBets.Tests.Fixtures;
using Fanex.Caching;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.SportTypeGroup
{
    [Collection("CacheCollection")]
    public class SportTypeGroupServiceImplTests
    {
        private const string SportGroupsCacheKey = "CN88.TotalBets.ServicesCN88.TotalBets.Services.SportGroupServiceImpl.SportGroups";
        private readonly CacheFixture cacheFixture;
        private readonly SportTypeGroupDataAccess sportTypeGroupDataAccess;
        private readonly CacheRefService cacheRefService;
        private readonly SportTypeGroupServiceImpl sportTypeGroupService;
        private IEnumerable<SportGroup> dataInCache;

        public SportTypeGroupServiceImplTests(CacheFixture cacheFixture)
        {
            this.cacheFixture = cacheFixture;
            dataInCache = new List<SportGroup> { new SportGroup(1, new List<int> { 1 }, (id) => "SportGroupName") };
            cacheFixture.CacheForTesting.TryGetAsync<IEnumerable<SportGroup>>(SportGroupsCacheKey)
                .Returns(_ => { return Tuple.Create(dataInCache != null, dataInCache); });
            ConfiguredCacheService.Remove(SportGroupsCacheKey);

            sportTypeGroupDataAccess = Substitute.For<SportTypeGroupDataAccess>();
            cacheRefService = Substitute.For<CacheRefService>();
            sportTypeGroupService = Substitute.ForPartsOf<SportTypeGroupServiceImpl>(sportTypeGroupDataAccess, cacheRefService);
        }

        [Fact]
        public void GetSportTypeGroup_NoDataInCache_CallsDataAccessToGetSportsGroup()
        {
            // Arrange
            dataInCache = null;

            // Act
            sportTypeGroupService.GetSportTypeGroups().ConfigureAwait(false);

            // Assert
            sportTypeGroupDataAccess.Received().GetSportGroups(Arg.Compat.Any<GetSportGroupsCriteria>());
        }

        [Fact]
        public void GetSportTypeGroup_DataInCache_CallsDataAccessToGetSportsGroup()
        {
            // Arrange
            var compareLogic = new CompareLogic();

            // Act
            var result = sportTypeGroupService.GetSportTypeGroups().GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(dataInCache, result).AreEqual);
        }

        [Fact]
        public async Task GetSportTypeGroupName_Always_CallsGetSportTypeGroups()
        {
            // Arrange
            var sportTypeGroup = 1;

            // Act
            await sportTypeGroupService.GetSportTypeGroupName(sportTypeGroup).ConfigureAwait(false);

            // Assert
            await sportTypeGroupService.Received().GetSportTypeGroups();
        }
    }
}