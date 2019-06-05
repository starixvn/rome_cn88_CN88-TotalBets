using AutoFixture;
using CN88.TotalBets.Features.Leagues.Criteria;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Leagues.Criteria
{
    public class GetLeagueCriteriaTests
    {
        private readonly Fixture fixture;

        public GetLeagueCriteriaTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectSetting()
        {
            // Arrange
            var criteria = new GetLeagueCriteria(fixture.Create<int>(), fixture.Create<int>());

            // Act & Assert
            Assert.Equal("CS_TotalBets_League_Sel", criteria.GetSettingKey());
        }

        [Fact]
        public void IsValid_Always_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetLeagueCriteria(fixture.Create<int>(), fixture.Create<int>());

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}