using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.ViewByBetTypes.Criteria;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.ViewByBetTypes.Criteria
{
    public class GetTotalBetsByBetTypeCriteriaTests
    {
        private readonly Fixture fixture;

        public GetTotalBetsByBetTypeCriteriaTests()
        {
            fixture = new Fixture();
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Bettype_Started_Super")]
        [InlineData(3, "CS_TotalBets_Bettype_Started_Master")]
        [InlineData(2, "CS_TotalBets_Bettype_Started_Agent")]
        public void GetSettingKey_EventTypeIsStarted_ReturnsCorrectSettingKey(byte userLevel, string expectedSettingKey)
        {
            // Arrange
            var criteria = new GetTotalBetsByBetTypeCriteria(
                Util.RandomValidCustId(fixture),
                0,
                false,
                Enumeration.FromValue<UserLevel>(userLevel),
                fixture.Create<int>(),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<decimal>(),
                EventTypeConstants.Started);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Bettype_Super")]
        [InlineData(3, "CS_TotalBets_Bettype_Master")]
        [InlineData(2, "CS_TotalBets_Bettype_Agent")]
        public void GetSettingKey_WithCorrectUserLevel_ReturnsCorrectSettingKey(byte userLevel, string expectedSettingKey)
        {
            // Arrange
            var criteria = new GetTotalBetsByBetTypeCriteria(
                Util.RandomValidCustId(fixture),
                0,
                false,
                Enumeration.FromValue<UserLevel>(userLevel),
                fixture.Create<int>(),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<decimal>(),
                EventTypeConstants.Outright);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetTotalBetsByBetTypeCriteria(
                custId,
                0,
                false,
                Util.RandomSMALevel(fixture),
                fixture.Create<int>(),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<decimal>(),
                EventTypeConstants.Outright);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidMatch_ReturnsFalse(int matchId)
        {
            // Arrange
            var criteria = new GetTotalBetsByBetTypeCriteria(
                Util.RandomValidCustId(fixture),
                0,
                false,
                Util.RandomSMALevel(fixture),
                matchId,
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<decimal>(),
                EventTypeConstants.Outright);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetTotalBetsByBetTypeCriteria(
                Util.RandomValidCustId(fixture),
                0,
                false,
                Util.RandomSMALevel(fixture),
                fixture.Create<Generator<int>>().First(x => x > 0),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<byte>(),
                fixture.Create<decimal>(),
                EventTypeConstants.Outright);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}