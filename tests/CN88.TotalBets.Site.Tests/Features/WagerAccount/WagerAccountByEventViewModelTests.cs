using System;
using System.Collections.Generic;
using System.Linq;
using CN88.TotalBets.Features.WagerAccount;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.WagerAccount;
using CN88.TotalBets.Site.Shared.Helpers;
using CN88.TotalBets.Site.Tests.Helpers;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.WagerAccount
{
    public class WagerAccountByEventViewModelTests
    {
        private readonly WagerAccountByEventQuery message;
        private readonly WagerAccountByEventViewModel wagerAccountByEventViewModel;

        public WagerAccountByEventViewModelTests()
        {
            UrlHelperTestSetUp.SetUp();

            var user = Substitute.For<IUser>();
            user.Id.Returns(1);
            user.Name.Returns("username");
            user.LevelId.Returns(UserLevel.Agent.Value);

            message = new WagerAccountByEventQuery(user)
            {
                MatchId = 1,
                StakeCondition = StakeConditionConstants.GreaterThan,
                Stake = 50,
                BetType = 1,
                LiveIndicator = MarketConstants.AllMarkets,
                BetTeam = string.Empty,
            };
            message.BreadcrumbLevel = new List<CustInfo>
            {
                new CustInfo(1,"custName",4,true)
            };
            wagerAccountByEventViewModel = new WagerAccountByEventViewModel(Enumerable.Empty<WagerAccountModel>(), message, false);
        }

        [Fact]
        public void GetWagerDetailUrl_Always_CallsUrlHelperAddQueryStringParamsFunc()
        {
            // Arrange
            var expectedRoutValues = new Dictionary<string, object>
            {
                ["CustId"] = message.ViewerId,
                ["CustName"] = message.ViewerName,
                ["MatchId"] = message.MatchId,
                ["StakeCondition"] = message.StakeCondition,
                ["Stake"] = message.Stake,
                ["CustLevelId"] = message.ViewerLevelId,
                ["BetType"] = message.BetType,
                ["LiveIndicator"] = message.LiveIndicator,
                ["BetTeam"] = message.BetTeam
            };

            // Act
            wagerAccountByEventViewModel.GetWagerDetailUrl();

            // Assert
            UrlHelper.AddQueryStringParamsFunc.Received().Invoke(
                "FeaturesPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRoutValues, routeValues).AreEqual));
        }

        [Theory]
        [InlineData(4, "Super")]
        [InlineData(3, "MA")]
        [InlineData(2, "Agent")]
        [InlineData(1, "Member")]
        public void GetUserLevelName_Always_ReturnsCorrectUserLevelName(byte custLevelValue, string expectedUserLevelName)
        {
            // Arrange
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelValue);

            // Act & Assert
            Assert.Equal(expectedUserLevelName, wagerAccountByEventViewModel.GetUserLevelName(custLevel));
        }

        [Fact]
        public void GetDownLineUrl_Always_CallsUrlHelperMapTotalBetsPathWithCorrectVirtualPath()
        {
            // Arrange
            message.CustLevelId = UserLevel.Super.Value;

            var custId = 1;
            var custName = "custname";

            // Act
            wagerAccountByEventViewModel.GetDownLineUrl(custId, custName);

            // Assert
            UrlHelper.MapTotalBetsPath.Received().Invoke("~/WagerAccount/GetByEvent");
        }

        [Fact]
        public void GetDownLineUrl_CustLevelIsAgentAndEventTypeIsStarted_CallsUrlHelperMapFeaturesPathWithCorrectVirtualPath()
        {
            // Arrange
            message.CustLevelId = UserLevel.Agent.Value;
            message.Type = EventTypeConstants.Started;

            var custId = 1;
            var custName = "custname";

            // Act
            wagerAccountByEventViewModel.GetDownLineUrl(custId, custName);

            // Assert
            UrlHelper.MapFeaturesPath.Received().Invoke("~/BetLists/StartedWagerByAccount");
        }

        [Fact]
        public void GetDownLineUrl_CustLevelIsAgentAndEventTypeIsNotStarted_CallsUrlHelperMapFeaturesPathWithCorrectVirtualPath()
        {
            // Arrange
            message.CustLevelId = UserLevel.Agent.Value;
            message.Type = EventTypeConstants.InPlay;

            var custId = 1;
            var custName = "custname";

            // Act
            wagerAccountByEventViewModel.GetDownLineUrl(custId, custName);

            // Assert
            UrlHelper.MapFeaturesPath.Received().Invoke("~/BetLists/EventWagerByAccount");
        }

        [Fact]
        public void GetDownLineUrl_CustLevelIsAgent_CallsUrlHelperAddQueryStringParamsFunc()
        {
            // Arrange
            message.CustLevelId = UserLevel.Agent.Value;
            message.Type = EventTypeConstants.InPlay;
            var custId = 1;
            var custName = "custname";
            JsonHelper.ToJsonFunc = Substitute.For<Func<object, string>>();
            JsonHelper.ToJsonFunc(Arg.Compat.Any<object>()).Returns("Json");
            var expectedRouteValues = new Dictionary<string, object>
            {
                ["CustId"] = custId,
                ["CustName"] = custName,
                ["MatchId"] = message.MatchId,
                ["StakeCondition"] = message.StakeCondition,
                ["Stake"] = message.Stake,
                ["CustLevelId"] = UserLevel.Member.Value,
                ["BetType"] = message.BetType,
                ["LiveIndicator"] = message.LiveIndicator,
                ["BetTeam"] = message.BetTeam,
                ["Type"] = message.Type,
                ["BreadcrumbLevelJson"] = "Json"
            };

            // Act
            wagerAccountByEventViewModel.GetDownLineUrl(custId, custName);

            // Assert
            UrlHelper.AddQueryStringParamsFunc.Received().Invoke(
                "FeaturesPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRouteValues, routeValues).AreEqual));
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        public void GetDownLineUrl_CustLevelIsNOTAgent_CallsUrlHelperAddQueryStringParamsFunc(byte custLevelId)
        {
            // Arrange
            message.CustLevelId = custLevelId;
            message.Type = EventTypeConstants.InPlay;
            var custId = 1;
            var custName = "custname";
            JsonHelper.ToJsonFunc = Substitute.For<Func<object, string>>();
            JsonHelper.ToJsonFunc(Arg.Compat.Any<object>()).Returns("Json");
            var expectedRouteValues = new Dictionary<string, object>
            {
                ["CustId"] = custId,
                ["CustName"] = custName,
                ["MatchId"] = message.MatchId,
                ["StakeCondition"] = message.StakeCondition,
                ["Stake"] = message.Stake,
                ["CustLevelId"] = (byte)(custLevelId - 1),
                ["BetType"] = message.BetType,
                ["LiveIndicator"] = message.LiveIndicator,
                ["BetTeam"] = message.BetTeam,
                ["Type"] = message.Type,
                ["BreadcrumbLevelJson"] = "Json"
            };

            // Act
            wagerAccountByEventViewModel.GetDownLineUrl(custId, custName);

            // Assert
            UrlHelper.AddQueryStringParamsFunc.Received().Invoke(
                "TotalBetsPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRouteValues, routeValues).AreEqual));
        }
    }
}