using System.Linq;
using CN88.TotalBets.Features.WagerAccount;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.WagerAccount;
using CN88.Web.Helpers;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.WagerAccount
{
    public class WagerAccountHandlerTests
    {
        private readonly IUser user;
        private readonly BrowserHelper browserHelper;
        private readonly IWagerAccountService wagerAccountService;
        private readonly WagerAccountHandler handler;
        private readonly CompareLogic compareLogic;

        public WagerAccountHandlerTests()
        {
            user = Substitute.For<IUser>();
            user.Id.Returns(2);
            user.LevelId.Returns(UserLevel.Agent.Value);

            compareLogic = new CompareLogic();
            browserHelper = Substitute.For<BrowserHelper>();
            wagerAccountService = Substitute.For<IWagerAccountService>();
            handler = new WagerAccountHandler(wagerAccountService, browserHelper);
        }

        [Fact]
        public void HandleWagerAccountByOutrightQuery_Always_CallsGetByOutright()
        {
            // Arrange
            var message = new WagerAccountByOutrightQuery(user);
            var param = new WagerAccountParam(
                message.CustId,
                message.SubAccountId,
                false,
                message.MatchId,
                message.Stake,
                message.StakeCondition,
                message.CustLevelId);

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            wagerAccountService.GetByOutright(Arg.Compat.Is<WagerAccountParam>(
                x => compareLogic.Compare(x, param).AreEqual));
        }

        [Fact]
        public void HandleWagerAccountByOutrightQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new WagerAccountByOutrightQuery(user);
            browserHelper.IsMobileTablet().Returns(false);
            wagerAccountService.GetByOutright(Arg.Compat.Any<WagerAccountParam>())
                .Returns(Enumerable.Empty<WagerAccountModel>());
            var expectedViewModel = new WagerAccountByOutrightViewModel(
                Enumerable.Empty<WagerAccountModel>(),
                message,
                false);

            // Act
            var actualViewModel = handler.Handle(message).GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }

        [Fact]
        public void HandleWagerAccountByEventQuery_Always_CallsGetByEvent()
        {
            // Arrange
            var message = new WagerAccountByEventQuery(user);
            var param = new WagerAccountByEventParam(
                message.CustId,
                message.SubAccountId,
                false,
                message.MatchId,
                message.Stake,
                message.StakeCondition,
                message.CustLevelId,
                message.BetType,
                message.BetTeam,
                message.LiveIndicator,
                message.Type);

            // Act
            handler.Handle(message).ConfigureAwait(false);

            // Assert
            wagerAccountService.GetByEvent(Arg.Compat.Is<WagerAccountByEventParam>(
                x => compareLogic.Compare(x, param).AreEqual));
        }

        [Fact]
        public void HandleWagerAccountByEventQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new WagerAccountByEventQuery(user);
            browserHelper.IsMobileTablet().Returns(false);
            wagerAccountService.GetByEvent(Arg.Compat.Any<WagerAccountByEventParam>())
                .Returns(Enumerable.Empty<WagerAccountModel>());
            var expectedViewModel = new WagerAccountByEventViewModel(
                Enumerable.Empty<WagerAccountModel>(),
                message,
                false);

            // Act
            var actualViewModel = handler.Handle(message).GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }
    }
}