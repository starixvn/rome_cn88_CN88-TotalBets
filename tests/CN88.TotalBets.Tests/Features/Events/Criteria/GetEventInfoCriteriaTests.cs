using AutoFixture;
using CN88.TotalBets.Features.Events.Criteria;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Events.Criteria
{
    [Trait("Category", "Event")]
    public class GetEventInfoCriteriaTests
    {
        private readonly Fixture fixture;

        public GetEventInfoCriteriaTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectSettingKey()
        {
            // Arrange
            var criteria = new GetEventInfoCriteria(fixture.Create<int>());

            // Act & Assert
            Assert.Equal("CS_TotalBets_EventInfo", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidMatchId_ReturnsFalse(int matchId)
        {
            // Arrange
            var criteria = new GetEventInfoCriteria(matchId);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetEventInfoCriteria(1);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}