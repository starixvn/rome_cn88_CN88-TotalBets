using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CN88.TotalBets.Features.Leagues;
using CN88.TotalBets.Features.Leagues.Models;
using CN88.TotalBets.Features.SportTypeGroup;
using CN88.TotalBets.Features.Started;
using CN88.TotalBets.Features.Started.Models;
using CN88.TotalBets.Features.Started.Params;
using CN88.TotalBets.Features.Summaries;
using CN88.TotalBets.Features.Summaries.Models;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Started;
using CN88.TotalBets.Site.Features.Started.ViewModels;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.Web.Helpers;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Started
{
    [Trait("Category", "Started")]
    public class StartedHandlerTest
    {
        private readonly IUser user;
        private readonly StartedService startedService;
        private readonly LeagueService leagueService;
        private readonly SummaryService summaryService;
        private readonly SportTypeGroupService sportTypeGroupService;
        private readonly BrowserHelper browserHelper;
        private readonly StartedHandler handler;

        public StartedHandlerTest()
        {
            startedService = Substitute.For<StartedService>();
            leagueService = Substitute.For<LeagueService>();
            summaryService = Substitute.For<SummaryService>();
            sportTypeGroupService = Substitute.For<SportTypeGroupService>();
            browserHelper = Substitute.For<BrowserHelper>();

            handler = new StartedHandler(
                startedService,
                leagueService,
                summaryService,
                sportTypeGroupService,
                browserHelper);

            user = Substitute.For<IUser>();
            user.Id.Returns(2);
            user.LevelId.Returns(UserLevel.Agent.Value);

            UrlHelperTestSetUp.SetUp();
        }

        [Fact]
        public void HandlerStartedQuery_Always_CallsGetGetStarted()
        {
            // Arrange
            var message = new StartedQuery(user);

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            startedService.Received().GetStarted(
                Arg.Compat.Is<StartedParam>(
                    x => x.CustId == message.ViewerId
                        && x.UserLevel == message.ActorLevel
                        && x.SportTypeGroup == message.SportTypeGroup
                        && x.LeagueId == message.LeagueId
                        && x.StakeCondition == message.StakeCondition
                        && x.Stake == message.Stake
                        && x.PossType == message.PossType
                        && x.Market == message.Market));
        }

        [Fact]
        public void HandlerStartedQuery_Always_CallsGetLeagues()
        {
            // Arrange
            var message = new StartedQuery(user);
            message.SportTypeGroup = 1;

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            leagueService.Received().GetLeagues(message.SportTypeGroup, EventTypeConstants.Started);
        }

        [Fact]
        public void HandlerStartedQuery_Always_CallsGetSportGroupSummaries()
        {
            // Arrange
            var message = new StartedQuery(user);
            message.SportTypeGroup = 1;

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            summaryService.Received().GetSportGroupSummaries(message.ViewerId, message.ActorLevel, EventTypeConstants.Started);
        }

        [Fact]
        public void HandlerStartedQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new StartedQuery(user);
            message.SportTypeGroup = 1;
            sportTypeGroupService.GetSportTypeGroupName(Arg.Compat.Any<int>())
                .Returns("SportTypeGroupName");
            var data = new List<StartedLeague>
            {
                new StartedLeague(1,"LeagueName",Enumerable.Empty<StartedMatch>())
            };
            startedService.GetStarted(Arg.Compat.Any<StartedParam>())
                .Returns(data);
            summaryService.GetSportGroupSummaries(message.ViewerId, message.ActorLevel, EventTypeConstants.Started)
                .Returns(Enumerable.Empty<SportGroupSummary>());
            leagueService.GetLeagues(message.SportTypeGroup, EventTypeConstants.Started)
                .Returns(Enumerable.Empty<League>());
            var expectedViewModel = new StartedViewModel(
                message,
                "SportTypeGroupName",
                data,
                Enumerable.Empty<SportGroupSummary>(),
                new List<SelectListItem> {
                    new SelectListItem { Value = "0", Text = "All Leagues", Selected = true }
                },
                browserHelper);

            // Act
            var actualViewModel = handler.Handle(message).GetAwaiter().GetResult();

            // Assert
            Assert.True(new CompareLogic().Compare(expectedViewModel, actualViewModel).AreEqual);
        }
    }
}