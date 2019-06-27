using CN88.TotalBets.Features.Started.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Started.Models
{
    public class StartedMatchTests
    {
        [Fact]
        public void TotalPMWager_Always_ReturnsCorrectValue()
        {
            // Arrange
            var match = new StartedMatch
            {
                PMStake = 10000,
                IPStake = 20000
            };

            // Act & Assert
            Assert.Equal(30000, match.TotalStake);
        }
    }
}