using AutoFixture;
using CN88.TotalBets.Criteria;
using CN88.TotalBets.Shared.Constants;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Overview.Criteria
{
    public class GetOverViewCriteriaTests
    {
        private readonly Fixture fixture;

        public GetOverViewCriteriaTests()
        {
            fixture = new Fixture();
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Overview_Super")]
        [InlineData(3, "CS_TotalBets_Overview_Master")]
        [InlineData(2, "CS_TotalBets_Overview_Agent")]
        public void GetSettingKey_WithCorrectRoleId_ReturnsCorrectSettingKey(byte roleId, string expectedSettingKey)
        {
            // Arrange
            var criteria = new GetOverViewCriteria(
                Util.RandomValidCustId(fixture),
                PossTypeConstants.Agent_Percentage,
                roleId);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetOverViewCriteria(
                custId,
                PossTypeConstants.Agent_Percentage,
                Util.RandomSMALevel(fixture).Value);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_InvalidPossType_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetOverViewCriteria(
                Util.RandomValidCustId(fixture),
                -1,
                Util.RandomSMALevel(fixture).Value);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetOverViewCriteria(
                Util.RandomValidCustId(fixture),
                1,
                Util.RandomSMALevel(fixture).Value);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}