using System;
using System.Collections.Generic;
using CN88.TotalBets.Features.Leagues;
using CN88.TotalBets.Features.Leagues.Criteria;
using CN88.TotalBets.Features.Leagues.Models;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Services;
using CN88.TotalBets.Tests.Fixtures;
using Fanex.Caching;
using Fanex.Data.Repository;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Leagues
{
    [Collection("CacheCollection")]
    public class LeagueServiceImplTests
    {
        private const string LeagueCacheKey = "CN88.TotalBets.Services.LeagueServiceImpl.Leagues";
        private readonly CacheFixture cacheFixture;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;
        private readonly LeagueServiceImpl leagueService;
        private int sportTypeGroup = 1;
        private int type = EventTypeConstants.InPlay;
        private IEnumerable<League> dataInCache;

        public LeagueServiceImplTests(CacheFixture cacheFixture)
        {
            this.cacheFixture = cacheFixture;
            dataInCache = new List<League>() { new League() };
            cacheFixture.CacheForTesting.TryGetAsync<IEnumerable<League>>($"{LeagueCacheKey}.{type}.{sportTypeGroup}")
                .Returns(_ => { return Tuple.Create(dataInCache != null, dataInCache); });
            ConfiguredCacheService.Remove($"{LeagueCacheKey}.{type}.{sportTypeGroup}");

            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheRefService = Substitute.For<CacheRefService>();
            leagueService = new LeagueServiceImpl(dynamicRepository, cacheRefService);
        }

        [Fact]
        public void GetLeagues_NoDataInCache_CallDynamicRepositoryFetchLeague()
        {
            // Arrange
            dataInCache = null;

            // Act
            leagueService.GetLeagues(sportTypeGroup, type).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().Fetch<League>(
                Arg.Compat.Is<GetLeagueCriteria>(
                    x => x.SportTypeGroup == sportTypeGroup
                        && x.Type == type));
        }

        [Fact]
        public void GetLeagues_DataInCache_ReturnDataGetFromCache()
        {
            // Arrage
            var compareLogic = new CompareLogic();

            // Act
            var result = leagueService.GetLeagues(sportTypeGroup, type).GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(dataInCache, result).AreEqual);
        }
    }
}