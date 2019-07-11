using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
using CN88.TotalBets.Features.ViewByBetTypes.Models;
using CN88.TotalBets.Shared.Constants;
using Xunit;

namespace CN88.TotalBets.Tests.Features.ViewByBetTypes.BetOptionBuilders
{
    public class BetOptionBuilder18Tests
    {
        private readonly BetOptionBuilder18 betOptionBuilder;

        public BetOptionBuilder18Tests()
        {
            betOptionBuilder = new BetOptionBuilder18();
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsO_ReturnsOver()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes._2nd_Over_Under, BetTeamValue.O, true);
            var match = new Match();
            var expectedResult = "Over";

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void BuildBetChoice_BetTeamIsU_ReturnsOver()
        {
            // Arrange
            var betOption = new BetOption((int)BetTypes._2nd_Over_Under, BetTeamValue.U, true);
            var match = new Match();
            var expectedResult = "Under";

            // Act
            var actualResult = betOptionBuilder.BuildBetChoice(betOption, match);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}