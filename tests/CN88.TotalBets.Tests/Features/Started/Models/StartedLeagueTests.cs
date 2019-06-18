using System.Collections.Generic;
using CN88.TotalBets.Features.Started.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Started.Models
{
    public class StartedLeagueTests
    {
        private readonly StartedLeague league;

        public StartedLeagueTests()
        {
            var matches = new List<StartedMatch>
            {
                new StartedMatch { PMWager = 5, PMStake = 5000, IPWager = 2, IPStake = 2000 },
                new StartedMatch { PMWager = 4, PMStake = 4000, IPWager = 3, IPStake = 3000 }
            };
            league = new StartedLeague(1, "V-League", matches);
        }

        [Fact]
        public void TotalPMWager_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(9, league.TotalPMWager);
        }

        [Fact]
        public void TotalPMStake_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(9000, league.TotalPMStake);
        }

        [Fact]
        public void TotalIPWager_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(5, league.TotalIPWager);
        }

        [Fact]
        public void TotalIPStake_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(5000, league.TotalIPStake);
        }
    }
}