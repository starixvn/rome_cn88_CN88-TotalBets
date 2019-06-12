using AutoFixture;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Outright;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.TotalBets.Tests;
using MediatR;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Outright
{
    [Trait("Category", "OutRight")]
    public class OutrightControllerTest
    {
        private readonly IUser user;
        private readonly IMediator mediator;
        private readonly Fixture fixture;
        private readonly OutrightController controller;

        public OutrightControllerTest()
        {
            user = Substitute.For<IUser>();
            mediator = Substitute.For<IMediator>();
            fixture = new Fixture();
            controller = new OutrightController(mediator);
            UrlHelperTestSetUp.SetUp();
        }

        [Fact]
        public void Index_ValidQuery_CallsMediatorSendEventQuery()
        {
            // Arrange
            user.Id.Returns(Util.RandomValidCustId(fixture));
            user.Language.Returns("en-US");
            var message = new OutrightQuery(user);

            // Act
            controller.Index(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }
    }
}