using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.Summaries.Criteria;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Summaries.Criteria
{
    public class GetSummaryBySportTypeGroupCriteriaTests
    {
        private readonly Fixture Fixture;

        public GetSummaryBySportTypeGroupCriteriaTests()
        {
            Fixture = new Fixture();
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Detail_Super")]
        [InlineData(3, "CS_TotalBets_Detail_Master")]
        [InlineData(2, "CS_TotalBets_Detail_Agent")]
        public void GetSettingKey_EventTypeIsNotStarted_ReturnValidSpName(byte custLevelValue, string spName)
        {
            // Arrange
            var custId = Fixture.Create<int>();
            var eventType = Fixture.Create<Generator<byte>>().First(x => x != EventTypeConstants.Started);
            var sportTypeGroup = Fixture.Create<byte>();
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelValue);
            var criteria = new GetSummaryBySportTypeGroupCriteria(custId, eventType, sportTypeGroup, custLevel);

            // Act
            var result = criteria.GetSettingKey();

            // Assert
            Assert.Equal(spName, result);
        }

        [Theory]
        [InlineData(4, "CS_TotalBets_Detail_Started_Super")]
        [InlineData(3, "CS_TotalBets_Detail_Started_Master")]
        [InlineData(2, "CS_TotalBets_Detail_Started_Agent")]
        public void GetSettingKey_EventTypeIsStarted_ReturnValidSpName(byte custLevelValue, string spName)
        {
            // Arrange
            var custId = Fixture.Create<int>();
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelValue);
            var sportTypeGroup = Fixture.Create<byte>();
            var criteria = new GetSummaryBySportTypeGroupCriteria(custId, EventTypeConstants.Started, sportTypeGroup, custLevel);

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
            var sportTypeGroup = Fixture.Create<byte>();
            var criteria = new GetSummaryBySportTypeGroupCriteria(custId, eventType, sportTypeGroup, custLevel);

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
            var sportTypeGroup = Fixture.Create<byte>();
            var criteria = new GetSummaryBySportTypeGroupCriteria(custId, eventType, sportTypeGroup, custLevel);

            // Act
            var result = criteria.IsValid();

            // Asssert
            Assert.True(result);
        }

        [Fact]
        public void SportTypeGroup_Always_Return_Data_Set_From_Ctor()
        {
            // Arrange
            var eventType = Fixture.Create<byte>();
            var custId = Util.RandomValidCustId(Fixture);
            var custLevel = Util.RandomSMALevel(Fixture);
            var sportTypeGroup = Fixture.Create<byte>();
            var criteria = new GetSummaryBySportTypeGroupCriteria(custId, eventType, sportTypeGroup, custLevel);

            // Act
            var result = criteria.SportTypeGroup;

            //Assert
            Assert.Equal(sportTypeGroup, result);
        }
    }
}