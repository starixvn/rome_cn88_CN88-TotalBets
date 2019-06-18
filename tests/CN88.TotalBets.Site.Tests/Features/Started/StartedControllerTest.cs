using AutoFixture;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Started;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.TotalBets.Tests;
using MediatR;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Started
{
    [Trait("Category", "Started")]
    public class StartedControllerTest
    {
        private readonly IUser user;
        private readonly IMediator mediator;
        private readonly Fixture fixture;
        private readonly StartedController controller;

        public StartedControllerTest()
        {
            user = Substitute.For<IUser>();
            mediator = Substitute.For<IMediator>();
            fixture = new Fixture();
            controller = new StartedController(mediator);
            UrlHelperTestSetUp.SetUp();
        }

        [Fact]
        public void Index_ValidQuery_CallsMediatorSendStartedQuery()
        {
            // Arrange
            user.Id.Returns(Util.RandomValidCustId(fixture));
            user.Language.Returns("en-US");
            var message = new StartedQuery(user);

            // Act
            controller.Index(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }
    }
}