using CN88.TotalBets.Features.Leagues.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Leagues.Models
{
    public class LeagueTests
    {
        private readonly League league;

        public LeagueTests()
        {
            league = new League(1, "V-League");
        }

        [Fact]
        public void Id_Always_RetrunsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(1, league.Id);
        }

        [Fact]
        public void Name_Always_RetrunsCorrectValue()
        {
            // Act & Assert
            Assert.Equal("V-League", league.Name);
        }
    }
}