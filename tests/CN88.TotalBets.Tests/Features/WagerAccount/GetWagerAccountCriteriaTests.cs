using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.WagerAccount;
using CN88.TotalBets.Shared.Constants;
using Xunit;

namespace CN88.TotalBets.Tests.Features.WagerAccount
{
    public class GetWagerAccountCriteriaTests
    {
        private readonly Fixture fixture;

        public GetWagerAccountCriteriaTests()
        {
            fixture = new Fixture();
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_WagerAccount_Outright_Master")]
        [InlineData(3, "CS_TotalBets_WagerAccount_Outright_Agent")]
        [InlineData(2, "CS_TotalBets_WagerAccount_Outright_Member")]
        public void GetSettingKey_WithCorrectRoleId_ReturnsCorrectSettingKey(byte roleId, string expectedSettingKey)
        {
            // Arrange
            var param = new WagerAccountParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                matchId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<decimal>(),
                roleId: roleId);

            var criteria = new GetWagerAccountCriteria(param);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            var param = new WagerAccountParam(
                custId: custId,
                subAccId: 0,
                isAllDownline: false,
                matchId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4);

            var criteria = new GetWagerAccountCriteria(param);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_StackeIsLessThanZero_ReturnsFalse()
        {
            var param = new WagerAccountParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                matchId: fixture.Create<int>(),
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: -1,
                roleId: 4);

            var criteria = new GetWagerAccountCriteria(param);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidMatchId_ReturnsFalse(int matchId)
        {
            var param = new WagerAccountParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                matchId: matchId,
                stakeCondition: StakeConditionConstants.GreaterThan,
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4);

            var criteria = new GetWagerAccountCriteria(param);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_InvalidStakeCondition_ReturnsFalse()
        {
            var param = new WagerAccountParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                matchId: fixture.Create<int>(),
                stakeCondition: fixture.Create<Generator<int>>().First(x => !StakeConditionConstants.Conditions.ContainsKey(x)),
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4);

            var criteria = new GetWagerAccountCriteria(param);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            var param = new WagerAccountParam(
                custId: Util.RandomValidCustId(fixture),
                subAccId: 0,
                isAllDownline: false,
                matchId: fixture.Create<int>(),
                stakeCondition: fixture.Create<Generator<int>>().First(x => StakeConditionConstants.Conditions.ContainsKey(x)),
                stake: fixture.Create<Generator<decimal>>().First(x => x >= 0),
                roleId: 4);

            var criteria = new GetWagerAccountCriteria(param);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}