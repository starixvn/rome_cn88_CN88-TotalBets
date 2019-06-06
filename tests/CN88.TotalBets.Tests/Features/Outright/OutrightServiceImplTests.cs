using AutoFixture;
using CN88.TotalBets.Features.Outright;
using CN88.TotalBets.Features.Outright.Criteria;
using CN88.TotalBets.Features.Outright.Models;
using CN88.TotalBets.Features.Outright.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Services;
using Fanex.Data.Repository;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Outright
{
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
    }
}