using System.Collections.Generic;
using CN88.TotalBets.Features.Overview.Models;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Site.Features.Overview.ViewModels;
using CN88.TotalBets.Site.Shared.Helpers;
using CN88.TotalBets.Site.Tests.Helpers;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Overview.ViewModels
{
    public class OverViewByParlayViewModelTests
    {
        private readonly OverViewByParlayViewModel overViewByParlayViewModel;

        public OverViewByParlayViewModelTests()
        {
            UrlHelperTestSetUp.SetUp();
            overViewByParlayViewModel = new OverViewByParlayViewModel(new OverViewByParlayModel(1, 2, 1, 2, 100, 200, 300));
        }

        [Fact]
        public void Url_Always_CallsUrlHelperAddQueryStringParamsFunc()
        {
            // Arrange
            int possType = 4;
            var sportTypeGroup = SportTypeConstants.Soccer;
            var expectedRouteValues = new Dictionary<string, object>
            {
                ["ViewMode"] = possType,
                ["SportGroupId"] = sportTypeGroup
            };

            // Act
            overViewByParlayViewModel.Url(possType, sportTypeGroup);

            // Assert
            UrlHelper.AddQueryStringParamsFunc.Received().Invoke(
                "FeaturesPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRouteValues, routeValues).AreEqual));
        }
    }
}