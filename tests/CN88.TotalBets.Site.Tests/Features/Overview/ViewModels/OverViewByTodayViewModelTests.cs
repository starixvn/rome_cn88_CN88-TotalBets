using System.Collections.Generic;
using System.Linq;
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
    public class OverViewByTodayViewModelTests
    {
        private readonly OverViewByTodayViewModel overViewByTodayViewModel;

        public OverViewByTodayViewModelTests()
        {
            UrlHelperTestSetUp.SetUp();
            overViewByTodayViewModel = new OverViewByTodayViewModel(Enumerable.Empty<OverViewBySportTypeModel>());
        }

        [Fact]
        public void Url_Always_CallsUrlHelperAddQueryStringParamsFunc()
        {
            // Arrange
            int possType = 4;
            var sportTypeGroup = SportTypeConstants.Soccer;
            var expectedRouteValues = new Dictionary<string, object>
            {
                ["PossType"] = possType,
                ["SportTypeGroup"] = sportTypeGroup,
                ["Type"] = EventTypeConstants.Today
            };

            // Act
            overViewByTodayViewModel.Url(possType, sportTypeGroup);

            // Assert
            UrlHelper.AddQueryStringParamsFunc.Received().Invoke(
                "TotalBetsPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRouteValues, routeValues).AreEqual));
        }
    }
}