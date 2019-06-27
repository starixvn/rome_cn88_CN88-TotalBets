using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CN88.TotalBets.Features.Started.Models;
using CN88.TotalBets.Features.Summaries.Models;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Started;
using CN88.TotalBets.Site.Features.Started.ViewModels;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.Web.Helpers;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Started.ViewModels
{
    public class StartedViewModelTests
    {
        private readonly IUser user;
        private readonly StartedQuery query;
        private readonly BrowserHelper browserHelper;

        public StartedViewModelTests()
        {
            UrlHelperTestSetUp.SetUp();

            user = Substitute.For<IUser>();
            user.Id.Returns(1);
            user.Name.Returns("UserName");
            user.LevelId.Returns(UserLevel.Agent.Value);

            query = new StartedQuery(user);
            browserHelper = Substitute.For<BrowserHelper>();
        }

        [Fact]
        public void BuildViewByBetTypeLink_Always_CallsReplaceOrAddQueryStringParamsFuncWithCorrectRouteValues()
        {
            // Arrange
            var matchId = 1;

            var startedViewModel = new StartedViewModel(
                query,
                string.Empty,
                Enumerable.Empty<StartedLeague>(),
                Enumerable.Empty<SportGroupSummary>(),
                Enumerable.Empty<SelectListItem>(),
                Substitute.For<BrowserHelper>());

            Site.Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc = Substitute.For<Func<string, IDictionary<string, object>, string>>();

            var expectedRouteValues = new Dictionary<string, object>
            {
                ["type"] = (byte)5,
                [nameof(matchId)] = matchId,
                [nameof(query.Stake)] = query.Stake,
                [nameof(query.StakeCondition)] = query.StakeCondition,
                [nameof(query.SportTypeGroup)] = query.SportTypeGroup,
                [nameof(query.PossType)] = query.PossType,
                [nameof(query.LeagueId)] = query.LeagueId,
                [nameof(query.IsAutoRefresh)] = query.IsAutoRefresh,
                [nameof(query.Market)] = query.Market,
                [nameof(query.DateType)] = query.DateType,
                ["IsShowKODate"] = true
            };

            // Act
            startedViewModel.BuildViewByBetTypeLink(5, matchId);

            // Assert
            Site.Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc.Received().Invoke(
                "TotalBetsPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRouteValues, routeValues).AreEqual));
        }
    }
}