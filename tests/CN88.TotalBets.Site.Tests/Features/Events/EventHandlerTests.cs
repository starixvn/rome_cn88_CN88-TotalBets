using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CN88.TotalBets.Features.Events;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.Events.Params;
using CN88.TotalBets.Features.Leagues;
using CN88.TotalBets.Features.Leagues.Models;
using CN88.TotalBets.Features.SportTypeGroup;
using CN88.TotalBets.Features.Summaries;
using CN88.TotalBets.Features.Summaries.Models;
using CN88.TotalBets.Features.Summaries.Params;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Events;
using CN88.TotalBets.Site.Features.Events.ViewModels;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.Web.Helpers;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Events
{
    [Trait("Category", "Event")]
    public class EventHandlerTests
    {
        private readonly IUser user;
        private readonly EventService eventService;
        private readonly LeagueService leagueService;
        private readonly SummaryService summaryService;
        private readonly SportTypeGroupService sportTypeGroupService;
        private readonly BrowserHelper browserHelper;
        private readonly EventHandler handler;
        private readonly CompareLogic compareLogic;

        public EventHandlerTests()
        {
            eventService = Substitute.For<EventService>();
            leagueService = Substitute.For<LeagueService>();
            summaryService = Substitute.For<SummaryService>();
            sportTypeGroupService = Substitute.For<SportTypeGroupService>();
            browserHelper = Substitute.For<BrowserHelper>();

            handler = new EventHandler(
                eventService,
                leagueService,
                summaryService,
                sportTypeGroupService,
                browserHelper);

            user = Substitute.For<IUser>();
            user.Id.Returns(2);
            user.LevelId.Returns(UserLevel.Agent.Value);

            UrlHelperTestSetUp.SetUp();
            compareLogic = new CompareLogic();
        }

        [Fact]
        public void HandlerEventQuery_Always_CallsGetEventsAsync()
        {
            // Arrange
            var message = new EventQuery(user);

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            eventService.Received().GetEventsAsync(
                Arg.Compat.Is<EventParam>(
                    x => x.CustId == message.ViewerId
                        && x.UserLevel == message.ActorLevel
                        && x.Type == message.Type
                        && x.SportTypeGroup == message.SportTypeGroup
                        && x.LeagueId == message.LeagueId
                        && x.StakeCondition == message.StakeCondition
                        && x.Stake == message.Stake
                        && x.PossType == message.PossType
                        && x.Market == message.Market));
        }

        [Fact]
        public void HandlerEventQuery_Always_CallsGetLeagues()
        {
            // Arrange
            var message = new EventQuery(user);
            message.SportTypeGroup = 1;
            message.Type = EventTypeConstants.InPlay;

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            leagueService.Received().GetLeagues(message.SportTypeGroup, message.Type);
        }

        [Fact]
        public void HandlerEventQuery_Always_CallsGetSportGroupSummaries()
        {
            // Arrange
            var message = new EventQuery(user);
            message.SportTypeGroup = 1;
            message.Type = EventTypeConstants.InPlay;
            var param = new SummaryByEventParam(
                message.ViewerId,
                message.SubAccountId,
                message.IsAllDownline,
                message.Type,
                message.ActorLevel);

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            summaryService.Received().GetSportGroupSummaries(
                Arg.Compat.Is<SummaryByEventParam>(x => compareLogic.Compare(x, param).AreEqual));
        }

        [Fact]
        public void HandlerEventQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new EventQuery(user);
            message.SportTypeGroup = 1;
            message.Type = EventTypeConstants.InPlay;
            sportTypeGroupService.GetSportTypeGroupName(Arg.Compat.Any<int>())
                .Returns("SportTypeGroupName");
            var eventModel = new MatchLeagueTotal(message.Type, Enumerable.Empty<MatchLeague>(), 2, 2000);
            eventService.GetEventsAsync(Arg.Compat.Any<EventParam>()).Returns(Task.FromResult(eventModel));
            summaryService.GetSportGroupSummaries(Arg.Compat.Any<SummaryByEventParam>())
                .Returns(Enumerable.Empty<SportGroupSummary>());
            leagueService.GetLeagues(message.SportTypeGroup, message.Type)
                .Returns(Enumerable.Empty<League>());
            var expectedViewModel = new EventViewModel(
                message,
                "SportTypeGroupName",
                eventModel,
                Enumerable.Empty<SportGroupSummary>(),
                new List<SelectListItem> {
                    new SelectListItem { Value = "0", Text = "All Leagues", Selected = true }
                },
                browserHelper);

            // Act
            var actualViewModel = handler.Handle(message).GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }
    }
}