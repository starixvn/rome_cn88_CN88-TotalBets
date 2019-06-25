using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
using CN88.TotalBets.Features.ViewByBetTypes.Models;
using CN88.TotalBets.Shared.Constants;
using Xunit;

namespace CN88.TotalBets.Tests.Features.ViewByBetTypes.BetOptionBuilders
{
    public class BetOptionBuilder3Tests
    {
        private readonly BetOptionBuilder3 betOptionBuilder;

        public BetOptionBuilder3Tests()
        {
            betOptionBuilder = new BetOptionBuilder3();
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsH_ReturnsOver()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.Over_Under, BetTeamValue.H, true);
            var match = new Match();
            var expectedResult = "Over";

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsA_ReturnsOver()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes.Over_Under, BetTeamValue.A, true);
            var match = new Match();
            var expectedResult = "Under";

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}