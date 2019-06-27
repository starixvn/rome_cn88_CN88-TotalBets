using System.Linq;
using System.Web.Mvc;
using CN88.TotalBets.Features.Outright.Models;
using CN88.TotalBets.Features.Summaries.Models;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Outright;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.Web.Helpers;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Outright
{
    public class OutrightViewModelTests
    {
        private readonly IUser user;
        private readonly OutrightQuery query;
        private readonly OutrightViewModel outrightViewModel;

        public OutrightViewModelTests()
        {
            UrlHelperTestSetUp.SetUp();

            user = Substitute.For<IUser>();
            user.Id.Returns(1);
            user.Name.Returns("UserName");
            user.LevelId.Returns(UserLevel.Agent.Value);

            query = new OutrightQuery(user);
            query.Stake = 50;
            query.StakeCondition = StakeConditionConstants.GreaterThan;

            outrightViewModel = new OutrightViewModel(
                query,
                string.Empty,
                Enumerable.Empty<TeamLeague>(),
                Enumerable.Empty<SportGroupSummary>(),
                Enumerable.Empty<SelectListItem>(),
                Substitute.For<BrowserHelper>());
        }

        [Fact]
        public void GetWagerAccountParams_Always_ReturnsCorrectValue()
        {
            // Arrange
            var matchId = 1;
            var expectedResult = new
            {
                custId = query.ViewerId,
                custLevelId = query.ViewerLevelId,
                custName = query.ViewerName,
                matchId,
                query.Stake,
                query.StakeCondition
            };

            // Act
            dynamic actualResult = outrightViewModel.GetWagerAccountParams(matchId);

            // Assert
            Assert.True(expectedResult.custId == actualResult.custId
                && expectedResult.custLevelId == actualResult.custLevelId
                && expectedResult.custName == actualResult.custName
                && expectedResult.matchId == actualResult.matchId
                && expectedResult.Stake == actualResult.Stake
                && expectedResult.StakeCondition == actualResult.StakeCondition);
        }
    }
}