using System.Linq;
using CN88.TotalBets.Features.Summaries.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Summaries.Models
{
    public class LeagueSummaryTests
    {
        [Fact]
        public void LeagueId_Always_ReturnsCorrectValue()
        {
            // Arrange
            var leagueSummary = new LeagueSummary(1, "leaguename", Enumerable.Empty<MatchSummary>());

            // Act & Assert
            Assert.Equal(1, leagueSummary.LeagueId);
        }
    }
}