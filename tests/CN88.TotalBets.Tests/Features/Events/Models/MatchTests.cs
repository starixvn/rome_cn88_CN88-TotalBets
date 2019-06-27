using System;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Shared.Services;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Events.Models
{
    public class MatchTests
    {
        public MatchTests()
        {
            CacheRefManager.CacheRefInstance = Substitute.For<CacheRefService>();
        }

        [Fact]
        public void HomeName_IsNeutral_ReturnsCorrectValue()
        {
            // Arrange
            var match = new Match(
                leagueId: 1,
                matchId: 1,
                showTimeDT: DateTime.Now,
                homeId: 1,
                awayId: 2,
                liveAwayScore: 0,
                liveHomeScore: 0,
                wager: 10,
                stake: 10000,
                betTypeCount: 2,
                sportTypeId: 1,
                isNeutral: true,
                offerLive: true);
            CacheRefManager.CacheRefInstance.GetTeamName(match.HomeId).Returns("HomeName");

            // Act & Assert
            Assert.Equal("HomeName (N)", match.HomeName);
        }

        [Fact]
        public void HomeName_IsNotNeutral_ReturnsCorrectValue()
        {
            // Arrange
            var match = new Match(
                leagueId: 1,
                matchId: 1,
                showTimeDT: DateTime.Now,
                homeId: 1,
                awayId: 2,
                liveAwayScore: 0,
                liveHomeScore: 0,
                wager: 10,
                stake: 10000,
                betTypeCount: 2,
                sportTypeId: 1,
                isNeutral: false,
                offerLive: true);
            CacheRefManager.CacheRefInstance.GetTeamName(match.HomeId).Returns("HomeName");

            // Act & Assert
            Assert.Equal("HomeName", match.HomeName);
        }

        [Fact]
        public void AwayName_Always_ReturnsCorrectValue()
        {
            // Arrange
            var match = new Match(
                leagueId: 1,
                matchId: 1,
                showTimeDT: DateTime.Now,
                homeId: 1,
                awayId: 2,
                liveAwayScore: 0,
                liveHomeScore: 0,
                wager: 10,
                stake: 10000,
                betTypeCount: 2,
                sportTypeId: 1,
                isNeutral: false,
                offerLive: true);
            CacheRefManager.CacheRefInstance.GetTeamName(match.AwayId).Returns("AwayName");

            // Act & Assert
            Assert.Equal("AwayName", match.AwayName);
        }
    }
}