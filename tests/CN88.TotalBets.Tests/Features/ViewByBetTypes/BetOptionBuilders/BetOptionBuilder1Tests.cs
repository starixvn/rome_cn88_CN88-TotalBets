using System;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
using CN88.TotalBets.Features.ViewByBetTypes.Models;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Services;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.ViewByBetTypes.BetOptionBuilders
{
    public class BetOptionBuilder1Tests
    {
        private readonly BetOptionBuilder1 betOptionBuilder;

        public BetOptionBuilder1Tests()
        {
            betOptionBuilder = new BetOptionBuilder1();
            CacheRefManager.CacheRefInstance = Substitute.For<CacheRefService>();
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsH_ReturnsHomeName()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.Handicap, BetTeamValue.H, true);
            var homeId = 1;
            var expectedResult = "HomeName";
            CacheRefManager.CacheRefInstance.GetTeamName(homeId).Returns(expectedResult);
            var match = new Match(1, 1, DateTime.Now, 1, 2, 1, 2, 5, 500, 2, 1, false, true);

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsA_ReturnsHomeName()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.Handicap, BetTeamValue.A, true);
            var awayId = 2;
            var expectedResult = "AwayName";
            CacheRefManager.CacheRefInstance.GetTeamName(awayId).Returns(expectedResult);
            var match = new Match(1, 1, DateTime.Now, 1, awayId, 1, 2, 5, 500, 2, 1, false, true);
            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}