using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoFixture;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.WagerAccount;
using CN88.TotalBets.Tests;
using CN88.Web.Helpers;
using MediatR;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.WagerAccount
{
    public class WagerAccountControllerTests
    {
        private readonly IUser user;
        private readonly IMediator mediator;
        private readonly BrowserHelper browserHelper;
        private readonly Fixture fixture;
        private readonly WagerAccountController controller;

        public WagerAccountControllerTests()
        {
            user = Substitute.For<IUser>();
            mediator = Substitute.For<IMediator>();
            browserHelper = Substitute.For<BrowserHelper>();
            fixture = new Fixture();

            controller = new WagerAccountController(mediator, browserHelper);
            controller.ControllerContext = FakeControllerContext(controller);
        }

        [Fact]
        public void GetByOutright_Always_CallsMediatorSendWagerAccountByOutrightQuery()
        {
            // Arrange
            user.Id.Returns(Util.RandomValidCustId(fixture));
            user.Language.Returns("en-US");
            var message = new WagerAccountByOutrightQuery(user);

            // Act
            controller.GetByOutright(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void GetByEvent_Always_CallsMediatorSendWagerAccountByEventQuery()
        {
            // Arrange
            user.Id.Returns(Util.RandomValidCustId(fixture));
            user.Language.Returns("en-US");
            var message = new WagerAccountByEventQuery(user);

            // Act
            controller.GetByEvent(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }

        private ControllerContext FakeControllerContext(ControllerBase controller)
        {
            var routeData = new RouteData();
            routeData.Values.Add("key1", "value1");
            return new ControllerContext(FakeHttpContext(), routeData, controller);
        }

        private HttpContextBase FakeHttpContext(string url = "")
        {
            var httpContextSub = Substitute.For<HttpContextBase>();
            var requestSub = Substitute.For<HttpRequestBase>();
            requestSub.Url.Returns(new Uri("http://localhost"));
            httpContextSub.Request.Returns(requestSub);

            return httpContextSub;
        }
    }
}