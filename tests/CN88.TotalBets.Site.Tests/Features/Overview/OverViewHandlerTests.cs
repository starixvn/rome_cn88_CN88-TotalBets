using CN88.TotalBets.Features.Overview;
using CN88.TotalBets.Features.SportTypeGroup;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Overview;
using CN88.Web.Helpers;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Overview
{
    public class OverViewHandlerTests
    {
        private readonly IUser user;
        private readonly IOverViewService overViewService;
        private readonly SportTypeGroupService sportTypeGroupService;
        private readonly BrowserHelper browserHelper;
        private readonly OverViewHandler handler;

        public OverViewHandlerTests()
        {
            user = Substitute.For<IUser>();
            overViewService = Substitute.For<IOverViewService>();
            sportTypeGroupService = Substitute.For<SportTypeGroupService>();
            browserHelper = Substitute.For<BrowserHelper>();
            handler = new OverViewHandler(overViewService, sportTypeGroupService, browserHelper);
        }

        [Fact]
        public void HandleOverViewQuery_Always_CallsOverViewServiceGet()
        {
            // Arrange
            user.Id.Returns(1);
            user.LevelId.Returns(UserLevel.Agent.Value);
            var message = new OverViewQuery(user);
            message.PossType = PossTypeConstants.Agent_Percentage;

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            overViewService.Received().Get(message.ViewerId, message.PossType, message.ViewerLevelId);
        }

        [Fact]
        public void HandleOverViewQuery_Always_CallsGetSportTypeGroups()
        {
            // Arrange
            var message = new OverViewQuery(user);

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            sportTypeGroupService.Received().GetSportTypeGroups();
        }
    }
}