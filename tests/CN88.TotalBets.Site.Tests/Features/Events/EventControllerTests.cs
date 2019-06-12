using AutoFixture;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Events;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.TotalBets.Tests;
using MediatR;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Events
{
    [Trait("Category", "Event")]
    public class EventControllerTests
    {
        private readonly IUser user;
        private readonly IMediator mediator;
        private readonly Fixture fixture;
        private readonly EventController controller;

        public EventControllerTests()
        {
            user = Substitute.For<IUser>();
            mediator = Substitute.For<IMediator>();
            fixture = new Fixture();
            controller = new EventController(mediator);
            UrlHelperTestSetUp.SetUp();
        }

        [Fact]
        public void Index_ValidQuery_CallsMediatorSendEventQuery()
        {
            // Arrange

            user.Id.Returns(Util.RandomValidCustId(fixture));
            user.Language.Returns("en-US");
            var message = new EventQuery(user);

            // Act
            controller.Index(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }
    }
}