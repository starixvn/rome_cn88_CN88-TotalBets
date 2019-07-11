using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
using CN88.TotalBets.Features.ViewByBetTypes.Models;
using CN88.TotalBets.Shared.Constants;
using Xunit;

namespace CN88.TotalBets.Tests.Features.ViewByBetTypes.BetOptionBuilders
{
    public class BetOptionBuilder428Tests
    {
        private readonly BetOptionBuilder428 betOptionBuilder;

        public BetOptionBuilder428Tests()
        {
            betOptionBuilder = new BetOptionBuilder428();
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsO_ReturnsOver()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes._2ndOddEven, BetTeamValue.O, true);
            var match = new Match();
            var expectedResult = "Odd";

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsE_ReturnsOver()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes._2ndOddEven, BetTeamValue.E, true);
            var match = new Match();
            var expectedResult = "Even";

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}