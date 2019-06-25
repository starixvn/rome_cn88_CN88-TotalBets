using CN88.TotalBets.Features.Summaries.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Summaries.Models
{
    public class MatchSummaryTests
    {
        private readonly MatchSummary matchSummary;

        public MatchSummaryTests()
        {
            matchSummary = new MatchSummary(1, 2, "homename", 3, "awayname", 4, 5000);
        }

        [Fact]
        public void MatchId_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(1, matchSummary.MatchId);
        }

        [Fact]
        public void HomeId_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(2, matchSummary.HomeId);
        }

        [Fact]
        public void HomeTeamName_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal("homename", matchSummary.HomeTeamName);
        }

        [Fact]
        public void AwayId_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(3, matchSummary.AwayId);
        }

        [Fact]
        public void AwayTeamName_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal("awayname", matchSummary.AwayTeamName);
        }
    }
}