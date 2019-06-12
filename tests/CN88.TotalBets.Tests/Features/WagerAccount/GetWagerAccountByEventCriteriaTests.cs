using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.WagerAccount;
using CN88.TotalBets.Shared.Constants;
using Xunit;

namespace CN88.TotalBets.Tests.Features.WagerAccount
{
    public class GetWagerAccountByEventCriteriaTests
    {
        private readonly Fixture fixture;

        public GetWagerAccountByEventCriteriaTests()
        {
            fixture = new Fixture();
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_WagerAccount_Started_Master")]
        [InlineData(3, "CS_TotalBets_WagerAccount_Started_Agent")]
        [InlineData(2, "CS_TotalBets_WagerAccount_Started_Member")]
        public void GetSettingKey_EventTypeIsStarted_ReturnsCorrectSettingKey(byte roleId, string expectedSettingKey)
        {
            // Arrange
            var param = new WagerAccountByEventParam(
                custId: Util.RandomValidCustId(fixture),
                matchId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<decimal>(),
                roleId: roleId,
                bettypeGroup: fixture.Create<int>(),
                betteam: fixture.Create<string>(),
                liveIndicator: fixture.Create<int>(),
                eventType: EventTypeConstants.Started);

            var criteria = new GetWagerAccountByEventCriteria(param);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_WagerAccount_Event_Master")]
        [InlineData(3, "CS_TotalBets_WagerAccount_Event_Agent")]
        [InlineData(2, "CS_TotalBets_WagerAccount_Event_Member")]
        public void GetSettingKey_WithCorrectRoleId_ReturnsCorrectSettingKey(byte roleId, string expectedSettingKey)
        {
            // Arrange
            var param = new WagerAccountByEventParam(
                custId: Util.RandomValidCustId(fixture),
                matchId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<decimal>(),
                roleId: roleId,
                bettypeGroup: fixture.Create<int>(),
                betteam: fixture.Create<string>(),
                liveIndicator: fixture.Create<int>(),
                eventType: EventTypeConstants.InPlay);

            var criteria = new GetWagerAccountByEventCriteria(param);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            var param = new WagerAccountByEventParam(
                custId: custId,
                matchId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4,
                bettypeGroup: fixture.Create<int>(),
                betteam: fixture.Create<string>(),
                liveIndicator: fixture.Create<int>(),
                eventType: EventTypeConstants.InPlay);

            var criteria = new GetWagerAccountByEventCriteria(param);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_StackeIsLessThanZero_ReturnsFalse()
        {
            var param = new WagerAccountByEventParam(
                custId: Util.RandomValidCustId(fixture),
                matchId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: -1,
                roleId: 4,
                bettypeGroup: fixture.Create<int>(),
                betteam: fixture.Create<string>(),
                liveIndicator: fixture.Create<int>(),
                eventType: EventTypeConstants.InPlay);

            var criteria = new GetWagerAccountByEventCriteria(param);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidMatchId_ReturnsFalse(int matchId)
        {
            var param = new WagerAccountByEventParam(
                custId: Util.RandomValidCustId(fixture),
                matchId: matchId,
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4,
                bettypeGroup: fixture.Create<int>(),
                betteam: fixture.Create<string>(),
                liveIndicator: fixture.Create<int>(),
                eventType: EventTypeConstants.InPlay);

            var criteria = new GetWagerAccountByEventCriteria(param);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_InvalidStakeCondition_ReturnsFalse()
        {
            var param = new WagerAccountByEventParam(
                custId: Util.RandomValidCustId(fixture),
                matchId: fixture.Create<int>(),
                stakeCondition: fixture.Create<Generator<int>>().First(x => !StakeConditionConstants.Conditions.ContainsKey(x)),
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4,
                bettypeGroup: fixture.Create<int>(),
                betteam: fixture.Create<string>(),
                liveIndicator: fixture.Create<int>(),
                eventType: EventTypeConstants.InPlay);

            var criteria = new GetWagerAccountByEventCriteria(param);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            var param = new WagerAccountByEventParam(
                custId: Util.RandomValidCustId(fixture),
                matchId: fixture.Create<int>(),
                stakeCondition: fixture.Create<Generator<int>>().First(x => StakeConditionConstants.Conditions.ContainsKey(x)),
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4,
                bettypeGroup: fixture.Create<int>(),
                betteam: fixture.Create<string>(),
                liveIndicator: fixture.Create<int>(),
                eventType: EventTypeConstants.InPlay);

            var criteria = new GetWagerAccountByEventCriteria(param);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}