using System.Collections.Generic;
using System.Linq;
using CN88.TotalBets.Features.Events;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.SportTypeGroup;
using CN88.TotalBets.Features.Summaries;
using CN88.TotalBets.Features.Summaries.Models;
using CN88.TotalBets.Features.Summaries.Params;
using CN88.TotalBets.Features.ViewByBetTypes;
using CN88.TotalBets.Features.ViewByBetTypes.Models;
using CN88.TotalBets.Features.ViewByBetTypes.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.ViewByBetType;
using CN88.TotalBets.Site.Features.ViewByBetType.ViewModels;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.Web.Helpers;
using KellermanSoftware.CompareNetObjects;
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
            summaryService.Received().GetSportGroupSummary(
                Arg.Compat.Is<SummaryBySportGroupParam>(
                    x => x.CustId == message.ViewerId
                        && x.SubAccId == message.SubAccountId
                        && x.IsAllDownline == message.IsAllDownline
                        && x.CustLevel == message.ActorLevel
                        && x.EventType == message.Type
                        && x.SportGroup == message.SportTypeGroup));
        }

        [Fact]
        public void HandleViewByBetTypeQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ViewByBetTypeQuery(user);
            message.MatchId = 1;
            message.Type = EventTypeConstants.Today;
            message.SportTypeGroup = 1;

            var match = new Match();
            eventService.GetMatch(message.MatchId).Returns(match);

            var totalBetByBetTypes = new Dictionary<int, TotalBetsByBetType>
            {
                [1] = new TotalBetsByBetType(1, 5, 5000, Enumerable.Empty<BetOptionGroup>())
            };
            viewByBetTypeService.GetTotalBetsByBetTypes(Arg.Compat.Any<ViewByBetTypeParam>())
                .Returns(totalBetByBetTypes);

            var sportGroupSummary = new SportGroupSummary();
            summaryService.GetSportGroupSummary(Arg.Compat.Any<SummaryBySportGroupParam>()).Returns(sportGroupSummary);

            sportTypeGroupService.GetSportTypeGroupName(message.SportTypeGroup).Returns("SportTypeGroupName");

            var expectedViewModel = new ViewByBetTypeViewModel(
                "SportTypeGroupName",
                totalBetByBetTypes.Values,
                match,
                sportGroupSummary,
                message, browserHelper);

            // Act
            var actualViewModel = handler.Handle(message).GetAwaiter().GetResult();

            // Assert
            Assert.True(new CompareLogic().Compare(expectedViewModel, actualViewModel).AreEqual);
        }
    }
}