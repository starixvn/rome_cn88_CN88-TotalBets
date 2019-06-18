using AutoFixture;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Overview;
using CN88.TotalBets.Tests;
using MediatR;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Overview
{
    public class OverViewControllerTests
    {
        private readonly IUser user;
        private readonly IMediator mediator;
        private readonly Fixture fixture;
        private readonly OverViewController controller;

        public OverViewControllerTests()
        {
            user = Substitute.For<IUser>();
            mediator = Substitute.For<IMediator>();
            fixture = new Fixture();
            controller = new OverViewController(mediator);
        }

        [Fact]
        public void Index_ValidQuery_CallsMediatorSendOverViewQuery()
        {
            // Arrange
            user.Id.Returns(Util.RandomValidCustId(fixture));
            user.Language.Returns("en-US");
            var message = new OverViewQuery(user);

            // Act
            controller.Index(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }
    }
}