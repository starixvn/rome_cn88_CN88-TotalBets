using CN88.TotalBets.Features.Events;
using CN88.TotalBets.Features.SportTypeGroup;
using CN88.TotalBets.Features.Summaries;
using CN88.TotalBets.Features.ViewByBetTypes;
using CN88.TotalBets.Features.ViewByBetTypes.Params;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.ViewByBetType;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.Web.Helpers;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.ViewByBetType
{
    public class ViewByBetTypeHandlerTests
    {
        private readonly IUser user;
        private readonly ViewByBetTypeService viewByBetTypeService;
        private readonly EventService eventService;
        private readonly SummaryService summaryService;
        private readonly SportTypeGroupService sportTypeGroupService;
        private readonly BrowserHelper browserHelper;
        private readonly ViewByBetTypeHandler handler;

        public ViewByBetTypeHandlerTests()
        {
            viewByBetTypeService = Substitute.For<ViewByBetTypeService>();
            eventService = Substitute.For<EventService>();
            summaryService = Substitute.For<SummaryService>();
            sportTypeGroupService = Substitute.For<SportTypeGroupService>();
            browserHelper = Substitute.For<BrowserHelper>();

            handler = new ViewByBetTypeHandler(
                viewByBetTypeService,
                eventService,
                summaryService,
                sportTypeGroupService,
                browserHelper);

            user = Substitute.For<IUser>();
            user.Id.Returns(2);
            user.LevelId.Returns(UserLevel.Agent.Value);

            UrlHelperTestSetUp.SetUp();
        }

        [Fact]
        public void HandleViewByBetTypeQuery_Always_CallsGetMatch()
        {
            // Arrange
            var message = new ViewByBetTypeQuery(user);
            message.MatchId = 1;

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            eventService.Received().GetMatch(message.MatchId);
        }

        [Fact]
        public void HandleViewByBetTypeQuery_Always_CallsGetTotalBetsByBetTypes()
        {
            // Arrange
            var message = new ViewByBetTypeQuery(user);
            message.MatchId = 1;

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            viewByBetTypeService.Received().GetTotalBetsByBetTypes(
                Arg.Compat.Is<ViewByBetTypeParam>(
                    x => x.CustId == message.ViewerId
                        && x.UserLevel == message.ActorLevel
                        && x.MatchId == message.MatchId
                        && x.StakeCondition == message.StakeCondition
                        && x.Stake == message.Stake
                        && x.PossType == message.PossType
                        && x.MarketType == message.Market
                        && x.EventType == message.Type));
        }

        [Fact]
        public void HandleViewByBetTypeQuery_Always_CallsGetSportGroupSummary()
        {
            // Arrange
            var message = new ViewByBetTypeQuery(user);
            message.MatchId = 1;
            message.SportTypeGroup = 1;

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            summaryService.Received().GetSportGroupSummary(message.ViewerId, message.ActorLevel, message.Type, message.SportTypeGroup);
        }
    }
}