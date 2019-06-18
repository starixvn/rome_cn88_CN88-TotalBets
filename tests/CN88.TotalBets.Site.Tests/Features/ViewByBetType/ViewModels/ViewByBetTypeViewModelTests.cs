using System;
using System.Linq;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.Summaries.Models;
using CN88.TotalBets.Features.ViewByBetTypes.Models;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.ViewByBetType;
using CN88.TotalBets.Site.Features.ViewByBetType.ViewModels;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.Web.Helpers;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.ViewByBetType.ViewModels
{
    public class ViewByBetTypeViewModelTests
    {
        private readonly IUser user;
        private readonly Match match;
        private readonly ViewByBetTypeQuery filterQuery;
        private readonly ViewByBetTypeViewModel viewByBetTypeViewModel;

        public ViewByBetTypeViewModelTests()
        {
            UrlHelperTestSetUp.SetUp();

            user = Substitute.For<IUser>();
            user.Id.Returns(1);
            user.Name.Returns("UserName");
            user.LevelId.Returns(UserLevel.Agent.Value);

            filterQuery = new ViewByBetTypeQuery(user);
            filterQuery.Type = EventTypeConstants.Started;

            match = new Match(1, 2, DateTime.Now, 1, 2, 0, 0, 2, 2000, 2, 1, false, true);
            viewByBetTypeViewModel = new ViewByBetTypeViewModel(
                string.Empty,
                Enumerable.Empty<TotalBetsByBetType>(),
                match,
                new SportGroupSummary(),
                filterQuery,
                Substitute.For<BrowserHelper>());
        }

        [Fact]
        public void GetWagerAccountParams_Always_ReturnsCorrectResult()
        {
            // Arrange
            var bettypeId = 1;
            var betTeam = "a";
            var liveIndicator = true;
            var expectedResult = new
            {
                custId = filterQuery.ViewerId,
                custLevelId = filterQuery.ViewerLevelId,
                custName = filterQuery.ViewerName,
                matchId = match.MatchId,
                filterQuery.Stake,
                filterQuery.StakeCondition,
                BetType = bettypeId,
                liveIndicator = 1,
                betTeam,
                type = filterQuery.Type
            };

            // Act
            dynamic actualResult = viewByBetTypeViewModel.GetWagerAccountParams(bettypeId, betTeam, liveIndicator);

            // Assert
            Assert.True(expectedResult.custId == actualResult.custId
                && expectedResult.custLevelId == actualResult.custLevelId
                && expectedResult.custName == actualResult.custName
                && expectedResult.matchId == actualResult.matchId
                && expectedResult.Stake == actualResult.Stake
                && expectedResult.StakeCondition == actualResult.StakeCondition
                && expectedResult.BetType == actualResult.BetType
                && expectedResult.liveIndicator == actualResult.liveIndicator
                && expectedResult.betTeam == actualResult.betTeam
                && expectedResult.type == actualResult.type);
        }

        [Theory]
        [InlineData(EventTypeConstants.InPlay)]
        [InlineData(EventTypeConstants.Started)]
        public void ShowScore_SportIsSoccerAndShowMarketStatus_ReturnsTrue(byte eventType)
        {
            // Arrange
            filterQuery.Type = eventType;
            var sportTypeId = SportTypeConstants.Soccer;

            // Act & Assert
            Assert.True(viewByBetTypeViewModel.ShowScore(sportTypeId));
        }
    }
}