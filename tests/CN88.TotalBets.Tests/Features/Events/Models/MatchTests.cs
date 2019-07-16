using System;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Shared.Services;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Events.Models
{
    public class MatchTests
    {
        private readonly int homeId;
        private readonly int awayId;

        public MatchTests()
        {
            homeId = 1;
            awayId = 2;
            CacheRefManager.CacheRefInstance = Substitute.For<CacheRefService>();
            CacheRefManager.CacheRefInstance.GetTeamName(homeId).Returns("HomeName");
            CacheRefManager.CacheRefInstance.GetTeamName(awayId).Returns("AwayName");
        }

        [Fact]
        public void HomeName_IsNeutral_ReturnsCorrectValue()
        {
            // Arrange
            var match = new Match(
                leagueId: 1,
                matchId: 1,
                showTimeDT: DateTime.Now,
                homeId: homeId,
                awayId: 2,
                liveAwayScore: 0,
                liveHomeScore: 0,
                wager: 10,
                stake: 10000,
                betTypeCount: 2,
                sportTypeId: 1,
                isNeutral: true,
                offerLive: true);

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
                homeId: homeId,
                awayId: 2,
                liveAwayScore: 0,
                liveHomeScore: 0,
                wager: 10,
                stake: 10000,
                betTypeCount: 2,
                sportTypeId: 1,
                isNeutral: false,
                offerLive: true);

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
                awayId: awayId,
                liveAwayScore: 0,
                liveHomeScore: 0,
                wager: 10,
                stake: 10000,
                betTypeCount: 2,
                sportTypeId: 1,
                isNeutral: false,
                offerLive: true);

            // Act & Assert
            Assert.Equal("AwayName", match.AwayName);
        }

        [Fact]
        public void ShowTimeDT_Always_ReturnsCorrectValue()
        {
            // Arrange
            var showTimeDT = new DateTime(2019, 7, 3);
            var match = new Match(
                leagueId: 1,
                matchId: 1,
                showTimeDT: showTimeDT,
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

            // Act & Assert
            Assert.Equal(showTimeDT, match.ShowTimeDT);
        }

        [Fact]
        public void LiveHomeScore_Always_ReturnsCorrectValue()
        {
            // Arrange
            var liveHomeScore = 2;
            var match = new Match(
                leagueId: 1,
                matchId: 1,
                showTimeDT: DateTime.Now,
                homeId: 1,
                awayId: 2,
                liveAwayScore: 0,
                liveHomeScore: liveHomeScore,
                wager: 10,
                stake: 10000,
                betTypeCount: 2,
                sportTypeId: 1,
                isNeutral: false,
                offerLive: true);

            // Act & Assert
            Assert.Equal(liveHomeScore, match.LiveHomeScore);
        }

        [Fact]
        public void LiveAwayScore_Always_ReturnsCorrectValue()
        {
            // Arrange
            var liveAwayScore = 2;
            var match = new Match(
                leagueId: 1,
                matchId: 1,
                showTimeDT: DateTime.Now,
                homeId: 1,
                awayId: 2,
                liveAwayScore: liveAwayScore,
                liveHomeScore: 0,
                wager: 10,
                stake: 10000,
                betTypeCount: 2,
                sportTypeId: 1,
                isNeutral: false,
                offerLive: true);

            // Act & Assert
            Assert.Equal(liveAwayScore, match.LiveAwayScore);
        }

        [Fact]
        public void BetTypeCount_Always_ReturnsCorrectValue()
        {
            // Arrange
            var betTypeCount = 2;
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
                betTypeCount: betTypeCount,
                sportTypeId: 1,
                isNeutral: false,
                offerLive: true);

            // Act & Assert
            Assert.Equal(betTypeCount, match.BetTypeCount);
        }

        [Fact]
        public void SportTypeId_Always_ReturnsCorrectValue()
        {
            // Arrange
            var sportTypeId = 2;
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
                betTypeCount: 1,
                sportTypeId: sportTypeId,
                isNeutral: false,
                offerLive: true);

            // Act & Assert
            Assert.Equal(sportTypeId, match.SportTypeId);
        }

        [Fact]
        public void OfferLive_Always_ReturnsCorrectValue()
        {
            // Arrange
            var offerLive = true;
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
                betTypeCount: 1,
                sportTypeId: 2,
                isNeutral: false,
                offerLive: offerLive);

            // Act & Assert
            Assert.Equal(offerLive, match.OfferLive);
        }
    }
}