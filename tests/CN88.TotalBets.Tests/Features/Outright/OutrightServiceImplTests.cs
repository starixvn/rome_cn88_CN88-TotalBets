using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.Outright;
using CN88.TotalBets.Features.Outright.Criteria;
using CN88.TotalBets.Features.Outright.Models;
using CN88.TotalBets.Features.Outright.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Services;
using Fanex.Data.Repository;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Outright
{
    [Trait("Category", "OutRight")]
    public class OutrightServiceImplTests
    {
        private readonly Fixture fixture;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;
        private readonly OutrightServiceImpl outrightService;

        public OutrightServiceImplTests()
        {
            fixture = new Fixture();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheRefService = Substitute.For<CacheRefService>();
            outrightService = new OutrightServiceImpl(dynamicRepository, cacheRefService);
        }

        [Fact]
        public void GetOutrightAsync_Always_CallDynamicFetchAsync()
        {
            // Arrange
            var param = new OutrightParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                userLevel: Util.RandomSMALevel(fixture),
                sportTypeGroup: fixture.Create<int>(),
                leagueId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<int>(),
                possType: PossTypeConstants.Agent_Percentage);

            // Act
            outrightService.GetOutrightsAsync(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().FetchAsync<Team>(
                Arg.Compat.Is<GetOutrightCriteria>(
                    x => x.CustId == param.CustId
                        && x.UserLevel == param.UserLevel
                        && x.SportTypeGroup == param.SportTypeGroup
                        && x.LeagueId == param.LeagueId
                        && x.StakeCondition == param.StakeCondition
                        && x.Stake == param.Stake
                        && x.PossType == param.PossType));
        }

        [Fact]
        public void GetOutrightAsync_NoDataReturnFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var param = new OutrightParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                userLevel: Util.RandomSMALevel(fixture),
                sportTypeGroup: fixture.Create<int>(),
                leagueId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<int>(),
                possType: PossTypeConstants.Agent_Percentage);
            dynamicRepository.FetchAsync<Team>(Arg.Compat.Any<GetOutrightCriteria>())
                .Returns(Enumerable.Empty<Team>());
            var expectedResult = Enumerable.Empty<TeamLeague>();

            // Act
            var actualResult = outrightService.GetOutrightsAsync(param)
                .GetAwaiter().GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetOutrightAsync_HasDataReturnFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            var param = new OutrightParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                userLevel: Util.RandomSMALevel(fixture),
                sportTypeGroup: fixture.Create<int>(),
                leagueId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<int>(),
                possType: PossTypeConstants.Agent_Percentage);
            var teams = new List<Team>
            {
                new Team(1,1,1,20,10000),
                new Team(1,2,2,10,20000)
            };
            dynamicRepository.FetchAsync<Team>(Arg.Compat.Any<GetOutrightCriteria>()).Returns(teams);
            cacheRefService.GetTeamName(Arg.Compat.Any<int>()).Returns("TeamName");
            cacheRefService.GetLeagueName(Arg.Compat.Any<int>()).Returns("LeagueName");
            var expectedResult = new List<TeamLeague>
            {
                new TeamLeague(1,"LeagueName",teams.OrderByDescending(x=>x.Stake),30,30000)
            };

            // Act
            var actualResult = outrightService.GetOutrightsAsync(param)
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }
    }
}