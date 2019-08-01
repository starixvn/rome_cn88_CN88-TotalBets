using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.Events.Criteria;
using CN88.TotalBets.Features.Events.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Events.Criteria
{
    [Trait("Category", "Event")]
    public class GetEventCriteriaTests
    {
        private readonly Fixture fixture;

        public GetEventCriteriaTests()
        {
            fixture = new Fixture();
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Event_Super")]
        [InlineData(3, "CS_TotalBets_Event_Master")]
        [InlineData(2, "CS_TotalBets_Event_Agent")]
        public void GetSettingKey_WithCorrectUserLevel_ReturnsCorrectSettingKey(byte userLevel, string expectedSettingKey)
        {
            // Arrange
            var custLevel = Enumeration.FromValue<UserLevel>(userLevel);
            var param = new EventParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                userLevel: custLevel,
                type: EventTypeConstants.InPlay,
                sportTypeGroup: fixture.Create<int>(),
                leagueId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<int>(),
                possType: fixture.Create<Generator<byte>>().First(x => PossTypeConstants.PossTypeByLevel[custLevel].ContainsKey(x)),
                market: MarketConstants.AllMarkets);

            var criteria = new GetEventCriteria(param);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var param = new EventParam(
                custId: custId,
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

            var criteria = new GetEventCriteria(param);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCustId_ReturnsTrue()
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

            var criteria = new GetEventCriteria(param);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}