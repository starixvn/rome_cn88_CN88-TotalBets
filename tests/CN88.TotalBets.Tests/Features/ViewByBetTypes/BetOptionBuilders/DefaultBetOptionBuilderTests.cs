using System.Collections.Generic;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
using CN88.TotalBets.Features.ViewByBetTypes.Models;
using CN88.TotalBets.Shared.Services;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.ViewByBetTypes.BetOptionBuilders
{
    public class DefaultBetOptionBuilderTests
    {
        private readonly string betTypeName;
        private readonly DefaultBetOptionBuilder defaultBetOptionBuilder;

        public DefaultBetOptionBuilderTests()
        {
            defaultBetOptionBuilder = new DefaultBetOptionBuilder();
            betTypeName = "BetTypeName";
            CacheRefManager.CacheRefInstance = Substitute.For<CacheRefService>();
            CacheRefManager.CacheRefInstance.GetBetTypeName(Arg.Compat.Any<int>()).Returns(betTypeName);
        }

        [Fact]
        public void BuildBetChoice_IsNotShowMarketStatus_ReturnsBetTypeName()
        {
            // Arrange
            var betOption = new BetOption(1, BetTeamValue.A, false);
            // Act
            var actualResult = defaultBetOptionBuilder.BuildBetChoice(betOption, new Match());

            // Assert
            Assert.Equal(betTypeName, actualResult);
        }

        [Fact]
        public void BuildBetChoice_IsShowMarketStatusAndBetOptionIsLive_ReturnsCorrectValue()
        {
            // Arrange
            var parameters = new Dictionary<string, object>
            {
                ["isShowMarketStatus"] = true
            };
            var betOption = new BetOption(1, BetTeamValue.A, true);
            var expectedResult = "BetTypeName (In-Play)";

            // Act
            var actualResult = defaultBetOptionBuilder.BuildBetChoice(betOption, new Match(), parameters);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void BuildBetChoice_IsShowMarketStatusAndBetOptionIsNotLive_ReturnsCorrectValue()
        {
            // Arrange
            var parameters = new Dictionary<string, object>
            {
                ["isShowMarketStatus"] = true
            };
            var betOption = new BetOption(1, BetTeamValue.A, false);
            var expectedResult = "BetTypeName (Pre-Match)";

            // Act
            var actualResult = defaultBetOptionBuilder.BuildBetChoice(betOption, new Match(), parameters);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}