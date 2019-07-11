using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
using CN88.TotalBets.Features.ViewByBetTypes.Models;
using CN88.TotalBets.Shared.Constants;
using Xunit;

namespace CN88.TotalBets.Tests.Features.ViewByBetTypes.BetOptionBuilders
{
    public class BetOptionBuilder2Tests
    {
        private readonly BetOptionBuilder2 betOptionBuilder;

        public BetOptionBuilder2Tests()
        {
            betOptionBuilder = new BetOptionBuilder2();
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsH_ReturnsOver()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.Odd_Even, BetTeamValue.H, true);
            var match = new Match();
            var expectedResult = "Odd";

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsA_ReturnsOver()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.Odd_Even, BetTeamValue.A, true);
            var match = new Match();
            var expectedResult = "Even";

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}