using AutoFixture;
using CN88.TotalBets.Features.Outright.Criteria;
using CN88.TotalBets.Features.Outright.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Outright.Criteria
{
    [Trait("Category", "OutRight")]
    public class GetOutrightCriteriaTests
    {
        private readonly Fixture fixture;

        public GetOutrightCriteriaTests()
        {
            fixture = new Fixture();
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Outright_Super")]
        [InlineData(3, "CS_TotalBets_Outright_Master")]
        [InlineData(2, "CS_TotalBets_Outright_Agent")]
        public void GetSettingKey_WithCorrectUserLevel_ReturnsCorrectSettingKey(byte userLevel, string expectedSettingKey)
        {
            // Arrange
            var param = new OutrightParam(
                custId: Util.RandomValidCustId(fixture),
                userLevel: Enumeration.FromValue<UserLevel>(userLevel),
                sportTypeGroup: fixture.Create<int>(),
                leagueId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<int>(),
                possType: PossTypeConstants.Agent_Percentage);
            var criteria = new GetOutrightCriteria(param);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }
    }
}