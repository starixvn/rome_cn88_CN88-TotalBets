using System.Collections.Generic;
using System.Linq;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Shared.Helpers;
using CN88.TotalBets.Site.Shared.ViewModels;
using KellermanSoftware.CompareNetObjects;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Shared.ViewModels
{
    public class BreadcrumbLevelViewModelTests
    {
        public BreadcrumbLevelViewModelTests()
        {
            UrlHelper.ReplaceOrAddQueryStringParamsFunc = (url, routeValues) => url;
            UrlHelper.GetSiteUrl = (levelId, query) => levelId + "?" + query;
        }

        [Fact]
        public void BuildBreadcrumbLinkByLevel_Always_RetrunsCorrectLink()
        {
            // Arrange
            var custInfo = new CustInfo(1, string.Empty, 1, true);
            var breadcrumbLevelViewModel = new BreadcrumbLevelViewModel(new List<CustInfo> { custInfo }, string.Empty);
            var query = "Query";
            var expectedLink = "Member?Query";
            var breadcrumbParamByLevel = new List<string> { "Member" };

            // Act
            var actualLink = breadcrumbLevelViewModel.BuildBreadcrumbLink(custInfo, query, breadcrumbParamByLevel, 0);

            // Assert
            Assert.Equal(expectedLink, actualLink);
        }

        [Fact]
        public void BuildBreadcrumbLinks_EmptyBreadcrumbLevel_ReturnsEmptyBreadcrumbItem()
        {
            // Arrange
            var custInfo = new CustInfo(1, "custName", 1, true);
            var breadcrumbLevelViewModel = new BreadcrumbLevelViewModel(new List<CustInfo> { custInfo }, string.Empty);
            var expectedResult = new List<BreadcrumbItem>
            {
                new BreadcrumbItem("custName","currentLink")
            };

            // Act
            var actualResult = breadcrumbLevelViewModel.BuildBreadcrumbLinks("currentLink");

            // Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult.ToList()).AreEqual);
        }

        [Fact]
        public void BuildBreadcrumbLinks_ExistBreadcrumbLevel_ReturnsEmptyBreadcrumbItem()
        {
            // Arrange
            var breadcrumbLevelViewModel = new BreadcrumbLevelViewModel(Enumerable.Empty<CustInfo>(), string.Empty);
            var expectedResult = Enumerable.Empty<BreadcrumbItem>();

            // Act
            var actualResult = breadcrumbLevelViewModel.BuildBreadcrumbLinks(string.Empty);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}