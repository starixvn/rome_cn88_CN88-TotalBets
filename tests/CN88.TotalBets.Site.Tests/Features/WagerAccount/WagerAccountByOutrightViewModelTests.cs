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
    public class WagerAccountByOutrightViewModelTests
    {
        private readonly WagerAccountByOutrightQuery message;
        private readonly WagerAccountByOutrightViewModel wagerAccountByOutrightViewModel;

        public WagerAccountByOutrightViewModelTests()
        {
            UrlHelperTestSetUp.SetUp();

            var user = Substitute.For<IUser>();
            user.Id.Returns(1);
            user.Name.Returns("username");
            user.LevelId.Returns(UserLevel.Agent.Value);

            message = new WagerAccountByOutrightQuery(user)
            {
                MatchId = 1,
                StakeCondition = StakeConditionConstants.GreaterThan,
                Stake = 50
            };
            message.BreadcrumbLevel = new List<CustInfo>
            {
                new CustInfo(1,"custName",4,true)
            };

            wagerAccountByOutrightViewModel = new WagerAccountByOutrightViewModel(Enumerable.Empty<WagerAccountModel>(), message, false);
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
                ["CustLevelId"] = message.ViewerLevelId
            };

            // Act
            wagerAccountByOutrightViewModel.GetWagerDetailUrl();

            // Assert
            UrlHelper.AddQueryStringParamsFunc.Received().Invoke(
                "FeaturesPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRoutValues, routeValues).AreEqual));
        }

        [Fact]
        public void GetDownLineUrl_Always_CallsUrlHelperMapTotalBetsPathWithCorrectVirtualPath()
        {
            // Arrange
            message.CustLevelId = UserLevel.Super.Value;

            var custId = 1;
            var custName = "custname";

            // Act
            wagerAccountByOutrightViewModel.GetDownLineUrl(custId, custName);

            // Assert
            UrlHelper.MapTotalBetsPath.Received().Invoke("~/WagerAccount/GetByOutright");
        }

        [Fact]
        public void GetDownLineUrl_CustLevelIsAgent_CallsUrlHelperMapFeaturesPathWithCorrectVirtualPath()
        {
            // Arrange
            message.CustLevelId = UserLevel.Agent.Value;

            var custId = 1;
            var custName = "custname";

            // Act
            wagerAccountByOutrightViewModel.GetDownLineUrl(custId, custName);

            // Assert
            UrlHelper.MapFeaturesPath.Received().Invoke("~/BetLists/OutrightWagerByAccount");
        }

        [Fact]
        public void GetDownLineUrl_CustLevelIsAgent_CallsUrlHelperAddQueryStringParamsFunc()
        {
            // Arrange
            message.CustLevelId = UserLevel.Agent.Value;
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
                ["BreadcrumbLevelJson"] = "Json"
            };

            // Act
            wagerAccountByOutrightViewModel.GetDownLineUrl(custId, custName);

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
                ["BreadcrumbLevelJson"] = "Json"
            };

            // Act
            wagerAccountByOutrightViewModel.GetDownLineUrl(custId, custName);

            // Assert
            UrlHelper.AddQueryStringParamsFunc.Received().Invoke(
                "TotalBetsPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRouteValues, routeValues).AreEqual));
        }
    }
}