using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Shared.Helpers;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Helpers
{
    public class JsonHelperTests
    {
        [Fact]
        public void ToJson_Always_ReturnsCorrectString()
        {
            // Arrange
            var data = new CustInfo(1, "CustName", 1, true);
            var expectedValue = "{'CustId':1,'CustName':'CustName','CustLevelId':1,'IsLink':true}";

            // Act
            var actualValue = JsonHelper.ToJson(data);

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}