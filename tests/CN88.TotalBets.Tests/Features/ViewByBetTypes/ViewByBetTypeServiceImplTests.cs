using System.Collections.Generic;
using AutoFixture;
using CN88.TotalBets.Features.ViewByBetTypes;
using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
using CN88.TotalBets.Features.ViewByBetTypes.Criteria;
using CN88.TotalBets.Features.ViewByBetTypes.Models;
using CN88.TotalBets.Features.ViewByBetTypes.Params;
using CN88.TotalBets.Shared.Constants;
using Fanex.Data.Repository;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.ViewByBetTypes
{
    public class ViewByBetTypeServiceImplTests
    {
        private readonly Fixture fixture;
        private readonly IDynamicRepository dynamicRepository;
        private readonly BetOptionBuilderFactory betOptionBuilderFactory;
        private readonly ViewByBetTypeServiceImpl viewByBetTypeService;

        public ViewByBetTypeServiceImplTests()
        {
            fixture = new Fixture();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            betOptionBuilderFactory = Substitute.For<BetOptionBuilderFactory>();
            viewByBetTypeService = new ViewByBetTypeServiceImpl(dynamicRepository, betOptionBuilderFactory);
        }

        [Fact]
        public void GetTotalBetsByBetTypes_Always_CallsDynamicRepositoryFetchAsync()
        {
            // Arrange
            var param = new ViewByBetTypeParam(
                Util.RandomValidCustId(fixture),
                0,
                false,
                Util.RandomSMALevel(fixture),
                fixture.Create<int>(),
                fixture.Create<byte>(),
                fixture.Create<decimal>(),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                EventTypeConstants.Started);
            var data = new List<BetOption>
            {
                new BetOption( 1, "a", true),
                new BetOption( 2, string.Empty, false)
            };
            dynamicRepository.FetchAsync<BetOption>(Arg.Compat.Any<GetTotalBetsByBetTypeCriteria>())
                .Returns(data);

            // Act
            viewByBetTypeService.GetTotalBetsByBetTypes(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().FetchAsync<BetOption>(
                Arg.Compat.Is<GetTotalBetsByBetTypeCriteria>(
                    x => x.CustId == param.CustId
                        && x.UserLevel == param.UserLevel
                        && x.MatchId == param.MatchId
                        && x.StakeCondition == param.StakeCondition
                        && x.Stake == param.Stake
                        && x.MarketType == param.MarketType
                        && x.PossType == param.PossType
                        && x.EventType == param.EventType));
        }
    }
}