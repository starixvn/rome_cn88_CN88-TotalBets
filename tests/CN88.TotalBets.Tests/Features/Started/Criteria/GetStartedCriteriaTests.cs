using AutoFixture;
using CN88.TotalBets.Features.Started.Criteria;
using CN88.TotalBets.Features.Started.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Started.Criteria
{
    public class GetStartedCriteriaTests
    {
        private readonly Fixture fixture;

        public GetStartedCriteriaTests()
        {
            fixture = new Fixture();
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Started_Super")]
        [InlineData(3, "CS_TotalBets_Started_Master")]
        [InlineData(2, "CS_TotalBets_Started_Agent")]
        public void GetSettingKey_WithCorrectUserLevel_ReturnsCorrectSettingKey(byte userLevel, string expectedSettingKey)
        {
            // Arrange
            var param = new StartedParam(
                Util.RandomValidCustId(fixture),
                Enumeration.FromValue<UserLevel>(userLevel),
                fixture.Create<int>(),
                fixture.Create<int>(),
                StakeConditionConstants.GreaterThan,
                fixture.Create<decimal>(),
                PossTypeConstants.Agent_Percentage,
                DateTypeConstants.AllDates,
                MarketConstants.AllMarkets);
            var criteria = new GetStartedCriteria(param);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }
    }
}