using System;
using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.Summaries.Criteria;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Summaries.Criteria
{
    public class GetSummaryByEventCriteriaTests
    {
        private readonly Fixture Fixture;

        public GetSummaryByEventCriteriaTests()
        {
            Fixture = new Fixture();
        }

        [Fact]
        public void GetSummaryByEventCriteria_Ctor_CustLevelIsMember_Throw_ArgumentOutOfRangeException()
        {
            var custId = Fixture.Create<int>();
            var eventType = Fixture.Create<byte>();
            var custLevel = UserLevel.Member;

            Assert.Throws<ArgumentOutOfRangeException>(nameof(custLevel),
                () => new GetSummaryByEventCriteria(custId, 0, false, eventType, custLevel));
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Summary_Super")]
        [InlineData(3, "CS_TotalBets_Summary_Master")]
        [InlineData(2, "CS_TotalBets_Summary_Agent")]
        public void GetSettingKey_EventTypeIsNotStarted_ReturnValidSpName(byte custLevelValue, string spName)
        {
            // Arrange
            var custId = Fixture.Create<int>();
            var eventType = Fixture.Create<Generator<byte>>().First(x => x != EventTypeConstants.Started);
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelValue);
            var criteria = new GetSummaryByEventCriteria(custId, 0, false, eventType, custLevel);

            // Act
            var result = criteria.GetSettingKey();

            // Assert
            Assert.Equal(spName, result);
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Summary_Started_Super")]
        [InlineData(3, "CS_TotalBets_Summary_Started_Master")]
        [InlineData(2, "CS_TotalBets_Summary_Started_Agent")]
        public void GetSettingKey_EventTypeIsStarted_ReturnValidSpName(byte custLevelValue, string spName)
        {
            // Arrange
            var custId = Fixture.Create<int>();
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelValue);
            var criteria = new GetSummaryByEventCriteria(custId, 0, false, EventTypeConstants.Started, custLevel);

            // Act
            var result = criteria.GetSettingKey();

            // Assert
            Assert.Equal(spName, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void IsValid_CustIdIsLessOrEqualToZero_Return_False(int custId)
        {
            // Arrange
            var eventType = Fixture.Create<byte>();
            var custLevel = Util.RandomSMALevel(Fixture);
            var criteria = new GetSummaryByEventCriteria(custId, 0, false, eventType, custLevel);

            // Act
            var result = criteria.IsValid();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_CustIdIsGreaterThanZero_Return_True()
        {
            // Arrange
            var eventType = Fixture.Create<byte>();
            var custId = Util.RandomValidCustId(Fixture);
            var custLevel = Util.RandomSMALevel(Fixture);
            var criteria = new GetSummaryByEventCriteria(custId, 0, false, eventType, custLevel);

            // Act
            var result = criteria.IsValid();

            // Asssert
            Assert.True(result);
        }
    }
}