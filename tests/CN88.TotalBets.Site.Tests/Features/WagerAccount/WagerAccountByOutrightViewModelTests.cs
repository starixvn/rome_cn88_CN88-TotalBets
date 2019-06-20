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
    }
}