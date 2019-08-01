using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.Events;
using CN88.TotalBets.Features.Events.Criteria;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.Events.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Services;
using Fanex.Data.Repository;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Events
{
    [Trait("Category", "Event")]
    public class EventServiceImplTests
    {
        private readonly Fixture fixture;
        private readonly CompareLogic compareLogic;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;
        private readonly EventServiceImpl eventService;

        public EventServiceImplTests()
        {
            fixture = new Fixture();
            compareLogic = new CompareLogic();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheRefService = Substitute.For<CacheRefService>();
            eventService = new EventServiceImpl(dynamicRepository, cacheRefService);
        }

        [Fact]
        public void GetEventsAsync_Always_CallsDynamicRepositoryFetchAsync()
        {
            // Arrange
            var param = new EventParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                userLevel: Util.RandomSMALevel(fixture),
                type: EventTypeConstants.InPlay,
                sportTypeGroup: fixture.Create<int>(),
                leagueId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<int>(),
                possType: PossTypeConstants.Agent_Percentage,
                market: MarketConstants.AllMarkets);

            // Act
            eventService.GetEventsAsync(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().FetchAsync<Match>(
                Arg.Compat.Is<GetEventCriteria>(
                    x => x.CustId == param.CustId
                        && x.UserLevel == param.UserLevel
                        && x.Type == param.Type
                        && x.SportTypeGroup == param.SportTypeGroup
                        && x.LeagueId == param.LeagueId
                        && x.StakeCondition == param.StakeCondition
                        && x.Stake == param.Stake
                        && x.PossType == param.PossType
                        && x.Market == param.Market));
        }

        [Fact]
        public void GetEventsAsync_EmptyDataReturnFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var param = new EventParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                userLevel: Util.RandomSMALevel(fixture),
                type: EventTypeConstants.InPlay,
                sportTypeGroup: fixture.Create<int>(),
                leagueId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<int>(),
                possType: PossTypeConstants.Agent_Percentage,
                market: MarketConstants.AllMarkets);
            var expectedResult = new MatchLeagueTotal(param.Type, Enumerable.Empty<MatchLeague>(), 0, 0);
            dynamicRepository.FetchAsync<Match>(Arg.Compat.Any<GetEventCriteria>())
                .Returns(Enumerable.Empty<Match>());

            // Act
            var actualResult = eventService.GetEventsAsync(param)
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void GetEventsAsync_HasDataReturnFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var param = new EventParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                userLevel: Util.RandomSMALevel(fixture),
                type: EventTypeConstants.InPlay,
                sportTypeGroup: fixture.Create<int>(),
                leagueId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<int>(),
                possType: PossTypeConstants.Agent_Percentage,
                market: MarketConstants.AllMarkets);

            var matches = new List<Match>
            {
                new Match(1,1,DateTime.Now,1,2,1,1,10,10000,2,1,false,true),
                new Match(1,2,DateTime.Now,1,2,1,1,10,20000,2,1,false,true)
            };
            dynamicRepository.FetchAsync<Match>(Arg.Compat.Any<GetEventCriteria>())
                .Returns(matches);
            cacheRefService.GetLeagueName(1).Returns("League Name");
            var matchLeagues = new List<MatchLeague>
            {
                new MatchLeague(1,"League Name",matches.OrderByDescending(x=>x.Stake),20,30000)
            };

            var expectedResult = new MatchLeagueTotal(
                param.Type,
                matchLeagues.OrderBy(x => x.LeagueName).ToList(),
                20,
                30000);

            // Act
            var actualResult = eventService.GetEventsAsync(param)
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void GetMatch_Always_CallDynamicRepositoryGetAsync()
        {
            // Arrange
            var matchId = fixture.Create<Generator<int>>().First(x => x > 0);

            // Act
            eventService.GetMatch(matchId).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().GetAsync<Match>(
                Arg.Compat.Is<GetEventInfoCriteria>(x => x.MatchId == matchId));
        }
    }
}