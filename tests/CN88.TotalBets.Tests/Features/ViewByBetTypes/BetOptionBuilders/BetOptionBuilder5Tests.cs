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
    public class BetOptionBuilder5Tests
    {
        private readonly BetOptionBuilder5 betOptionBuilder;

        public BetOptionBuilder5Tests()
        {
            betOptionBuilder = new BetOptionBuilder5();
            CacheRefManager.CacheRefInstance = Substitute.For<CacheRefService>();
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsNumberOne_ReturnsHomeName()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.FT_1X2, BetTeamValue.NumberOne, true);
            var expectedResult = "HomeName";
            CacheRefManager.CacheRefInstance.GetTeamName(Arg.Compat.Any<int>()).Returns(expectedResult);
            var match = new Match(1, 1, DateTime.Now, 1, 2, 1, 2, 5, 500, 2, 1, false, true);

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsNumberTwo_ReturnsHomeName()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.FT_1X2, BetTeamValue.NumberTwo, true);
            var expectedResult = "AwayName";
            CacheRefManager.CacheRefInstance.GetTeamName(Arg.Compat.Any<int>()).Returns(expectedResult);
            var match = new Match(1, 1, DateTime.Now, 1, 2, 1, 2, 5, 500, 2, 1, false, true);

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsX_ReturnsDraw()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.FT_1X2, BetTeamValue.X, true);
            var match = new Match();

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal("Draw", actualResult);
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsDefault_ReturnsBetTeam()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.FT_1X2, BetTeamValue.O, true);
            var match = new Match();

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal("o", actualResult);
        }
    }
}