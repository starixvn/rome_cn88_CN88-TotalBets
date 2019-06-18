using CN88.TotalBets.Features.Leagues;
using CN88.TotalBets.Features.Outright;
using CN88.TotalBets.Features.Outright.Params;
using CN88.TotalBets.Features.SportTypeGroup;
using CN88.TotalBets.Features.Summaries;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Outright;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.Web.Helpers;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Outright
{
    [Trait("Category", "OutRight")]
    public class OutrightHandlerTests
    {
        private readonly IUser user;
        private readonly OutrightService outrightService;
        private readonly LeagueService leagueService;
        private readonly SummaryService summaryService;
        private readonly SportTypeGroupService sportTypeGroupService;
        private readonly BrowserHelper browserHelper;
        private readonly OutrightHandler handler;

        public OutrightHandlerTests()
        {
            outrightService = Substitute.For<OutrightService>();
            leagueService = Substitute.For<LeagueService>();
            summaryService = Substitute.For<SummaryService>();
            sportTypeGroupService = Substitute.For<SportTypeGroupService>();
            browserHelper = Substitute.For<BrowserHelper>();

            handler = new OutrightHandler(
                outrightService,
                leagueService, summaryService,
                sportTypeGroupService,
                browserHelper);

            user = Substitute.For<IUser>();
            user.Id.Returns(2);
            user.LevelId.Returns(UserLevel.Agent.Value);

            UrlHelperTestSetUp.SetUp();
        }

        [Fact]
        public void HandlerOutrightQuery_Always_CallsGetOutrightsAsync()
        {
            // Arrange
            var message = new OutrightQuery(user);

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            outrightService.Received().GetOutrightsAsync(
                Arg.Compat.Is<OutrightParam>(
                    x => x.CustId == message.ViewerId
                        && x.UserLevel == message.ActorLevel
                        && x.SportTypeGroup == message.SportTypeGroup
                        && x.LeagueId == message.LeagueId
                        && x.StakeCondition == message.StakeCondition
                        && x.Stake == message.Stake
                        && x.PossType == message.PossType));
        }

        [Fact]
        public void HandlerOutrightQuery_Always_CallsGetLeagues()
        {
            // Arrange
            var message = new OutrightQuery(user);
            message.SportTypeGroup = 1;

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            leagueService.Received().GetLeagues(message.SportTypeGroup, EventTypeConstants.Outright);
        }

        [Fact]
        public void HandlerOutrightQuery_Always_CallsGetSportGroupSummaries()
        {
            // Arrange
            var message = new OutrightQuery(user);
            message.SportTypeGroup = 1;

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            summaryService.Received().GetSportGroupSummaries(message.ViewerId, message.ActorLevel, EventTypeConstants.Outright);
        }
    }
}