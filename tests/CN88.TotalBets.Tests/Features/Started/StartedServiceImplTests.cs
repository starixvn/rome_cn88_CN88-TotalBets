using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.Started;
using CN88.TotalBets.Features.Started.Criteria;
using CN88.TotalBets.Features.Started.Models;
using CN88.TotalBets.Features.Started.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Services;
using Fanex.Data.Repository;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Started
{
    [Trait("Category", "Started")]
    public class StartedServiceImplTests
    {
        private readonly Fixture fixture;
        private readonly CacheRefService cacheRefService;
        private readonly IDynamicRepository dynamicRepository;
        private readonly StartedServiceImpl startedService;

        public StartedServiceImplTests()
        {
            fixture = new Fixture();
            cacheRefService = Substitute.For<CacheRefService>();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            startedService = new StartedServiceImpl(cacheRefService, dynamicRepository);
        }

        [Fact]
        public void GetStarted_Always_CallsDynamicRepositoryFetchMultipleAsync()
        {
            // Arrange
            var param = new StartedParam(
                Util.RandomValidCustId(fixture),
                Util.RandomSMALevel(fixture),
                fixture.Create<int>(),
                fixture.Create<int>(),
                StakeConditionConstants.GreaterThan,
                fixture.Create<decimal>(),
                PossTypeConstants.Agent_Percentage,
                DateTypeConstants.AllDates,
                MarketConstants.AllMarkets);

            // Act
            startedService.GetStarted(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().FetchMultipleAsync<StartedMatch, StartedMatchSummary>(
                Arg.Compat.Is<GetStartedCriteria>(
                    x => x.CustId == param.CustId
                        && x.UserLevel == param.UserLevel
                        && x.SportTypeGroup == param.SportTypeGroup
                        && x.LeagueId == param.LeagueId
                        && x.StakeCondition == param.StakeCondition
                        && x.Stake == param.Stake
                        && x.PossType == param.PossType
                        && x.DateType == param.DateType
                        && x.Market == param.Market));
        }

        [Fact]
        public void GetStarted_NoDataReturnFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var param = new StartedParam(
                Util.RandomValidCustId(fixture),
                Util.RandomSMALevel(fixture),
                fixture.Create<int>(),
                fixture.Create<int>(),
                StakeConditionConstants.GreaterThan,
                fixture.Create<decimal>(),
                PossTypeConstants.Agent_Percentage,
                DateTypeConstants.AllDates,
                MarketConstants.AllMarkets);
            dynamicRepository.FetchMultipleAsync<StartedMatch, StartedMatchSummary>(Arg.Compat.Any<GetStartedCriteria>())
                .Returns(new Tuple<IEnumerable<StartedMatch>, IEnumerable<StartedMatchSummary>>(Enumerable.Empty<StartedMatch>(), Enumerable.Empty<StartedMatchSummary>()));
            var expectedResult = Enumerable.Empty<StartedLeague>();

            // Act
            var actualResult = startedService.GetStarted(param).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetStarted_HasDataReturnFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            var param = new StartedParam(
                Util.RandomValidCustId(fixture),
                Util.RandomSMALevel(fixture),
                fixture.Create<int>(),
                fixture.Create<int>(),
                StakeConditionConstants.GreaterThan,
                fixture.Create<decimal>(),
                PossTypeConstants.Agent_Percentage,
                DateTypeConstants.AllDates,
                MarketConstants.AllMarkets);
            var matches = new List<StartedMatch>
            {
                new StartedMatch(1,1,1,2,1,2,0,1,DateTime.Now,true,false,true,2),
                new StartedMatch(1,2,1,2,1,2,0,1,DateTime.Now,true,false,true,2)
            };
            var matchSummaries = new List<StartedMatchSummary>
            {
                new StartedMatchSummary(1,true,5,10000),
                new StartedMatchSummary(2,true,10,20000)
            };

            dynamicRepository.FetchMultipleAsync<StartedMatch, StartedMatchSummary>(Arg.Compat.Any<GetStartedCriteria>())
                .Returns(new Tuple<IEnumerable<StartedMatch>, IEnumerable<StartedMatchSummary>>(matches, matchSummaries));
            cacheRefService.GetLeagueName(Arg.Compat.Any<int>()).Returns("LeagueName");
            var expectedResult = new List<StartedLeague>
            {
                new StartedLeague(1,"LeagueName",matches.OrderByDescending(x=>x.TotalStake))
            }.OrderBy(x => x.LeagueName);

            // Act
            var actualResult = startedService.GetStarted(param).GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }
    }
}