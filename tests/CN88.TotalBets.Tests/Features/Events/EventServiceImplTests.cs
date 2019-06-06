using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.Events;
using CN88.TotalBets.Features.Events.Criteria;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.Events.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Services;
using Fanex.Data.Repository;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Events
{
    public class EventServiceImplTests
    {
        private readonly Fixture fixture;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;
        private readonly EventServiceImpl eventService;

        public EventServiceImplTests()
        {
            fixture = new Fixture();
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
                        && x.Stake == param.StakeCondition
                        && x.PossType == param.PossType
                        && x.Market == param.Market));
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