using CN88.TotalBets.Features.SportTypeGroup.Criteria;
using Xunit;

namespace CN88.TotalBets.Tests.Features.SportTypeGroup.Criteria
{
    public class GetSportGroupsCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectSettingKey()
        {
            // Arrange
            var criteria = new GetSportGroupsCriteria();

            // Act & Assert
            Assert.Equal("CS_SportType_Sel", criteria.GetSettingKey());
        }

        [Fact]
        public void IsValid_Always_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetSportGroupsCriteria();

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}