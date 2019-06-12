using AutoFixture;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.ViewByBetType;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.TotalBets.Tests;
using MediatR;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.ViewByBetType
{
    public class ViewByBetTypeControllerTests
    {
        private readonly IUser user;
        private readonly IMediator mediator;
        private readonly Fixture fixture;
        private readonly ViewByBetTypeController controller;

        public ViewByBetTypeControllerTests()
        {
            user = Substitute.For<IUser>();
            mediator = Substitute.For<IMediator>();
            fixture = new Fixture();
            controller = new ViewByBetTypeController(mediator);
            UrlHelperTestSetUp.SetUp();
        }

        [Fact]
        public void Index_ValidQuery_CallsMediatorSendViewByBetTypeQuery()
        {
            // Arrange
            user.Id.Returns(Util.RandomValidCustId(fixture));
            user.Language.Returns("en-US");
            var message = new ViewByBetTypeQuery(user);

            // Act
            controller.Index(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }
    }
}