using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.WagerAccount;
using CN88.TotalBets.Shared.Constants;
using Fanex.Data.Repository;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.WagerAccount
{
    public class WagerAccountServiceTests
    {
        private readonly Fixture fixture;
        private readonly IDynamicRepository dynamicRepository;
        private readonly WagerAccountService wagerAccountService;

        public WagerAccountServiceTests()
        {
            fixture = new Fixture();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            wagerAccountService = new WagerAccountService(dynamicRepository);
        }

        [Fact]
        public void GetByOutright_Always_CallsDynamicRepositoryFetchAsync()
        {
            // Arrange
            var param = new WagerAccountParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                matchId: fixture.Create<int>(),
                stakeCondition: fixture.Create<Generator<int>>().First(x => StakeConditionConstants.Conditions.ContainsKey(x)),
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4);

            // Act
            wagerAccountService.GetByOutright(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().FetchAsync<WagerAccountModel>(
                Arg.Compat.Is<GetWagerAccountCriteria>(
                    x => x.CustId == param.CustId
                        && x.MatchId == param.MatchId
                        && x.StakeCondition == param.StakeCondition
                        && x.Stake == param.Stake
                        && x.Role == param.Role));
        }

        [Fact]
        public void GetByEvent_Always_CallsDynamicRepositoryFetchAsync()
        {
            // Arrange
            var param = new WagerAccountByEventParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                matchId: fixture.Create<int>(),
                stakeCondition: fixture.Create<Generator<int>>().First(x => StakeConditionConstants.Conditions.ContainsKey(x)),
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4,
                bettypeGroup: fixture.Create<int>(),
                betteam: fixture.Create<string>(),
                liveIndicator: fixture.Create<int>(),
                eventType: fixture.Create<Generator<byte>>().First(x => EventTypeConstants.EventTypes.ContainsKey(x)));

            // Act
            wagerAccountService.GetByEvent(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().FetchAsync<WagerAccountModel>(
                Arg.Compat.Is<GetWagerAccountByEventCriteria>(
                    x => x.CustId == param.CustId
                        && x.MatchId == param.MatchId
                        && x.StakeCondition == param.StakeCondition
                        && x.Stake == param.Stake
                        && x.Role == param.Role
                        && x.BettypeGroup == param.BettypeGroup
                        && x.BetTeam == param.BetTeam
                        && x.LiveIndicator == param.LiveIndicator
                        && x.EventType == param.EventType));
        }

        [Fact]
        public void GetByEvent_Always_ReturnsCorrectResult()
        {
            // Arrange
            var param = new WagerAccountByEventParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                matchId: fixture.Create<int>(),
                stakeCondition: fixture.Create<Generator<int>>().First(x => StakeConditionConstants.Conditions.ContainsKey(x)),
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4,
                bettypeGroup: fixture.Create<int>(),
                betteam: fixture.Create<string>(),
                liveIndicator: fixture.Create<int>(),
                eventType: fixture.Create<Generator<byte>>().First(x => EventTypeConstants.EventTypes.ContainsKey(x)));
            var expectedResult = new List<WagerAccountModel>
            {
                new WagerAccountModel()
            };
            dynamicRepository.FetchAsync<WagerAccountModel>(Arg.Compat.Any<GetWagerAccountByEventCriteria>())
                .Returns(expectedResult);

            // Act
            var actualResult = wagerAccountService.GetByEvent(param).GetAwaiter().GetResult();

            // Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }
    }
}