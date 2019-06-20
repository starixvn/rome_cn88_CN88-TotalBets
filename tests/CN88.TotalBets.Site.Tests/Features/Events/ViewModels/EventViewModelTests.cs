using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CN88.TotalBets.Features.Events.Models;
using CN88.TotalBets.Features.Summaries.Models;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Features.Events;
using CN88.TotalBets.Site.Features.Events.ViewModels;
using CN88.TotalBets.Site.Tests.Helpers;
using CN88.Web.Helpers;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Events.ViewModels
{
    public class EventViewModelTests
    {
        private readonly IUser user;
        private readonly EventQuery query;
        private readonly BrowserHelper browserHelper;

        public EventViewModelTests()
        {
            UrlHelperTestSetUp.SetUp();

            user = Substitute.For<IUser>();
            user.Id.Returns(1);
            user.Name.Returns("UserName");
            user.LevelId.Returns(UserLevel.Agent.Value);

            query = new EventQuery(user);
            browserHelper = Substitute.For<BrowserHelper>();
        }

        [Fact]
        public void BuildViewByBetTypeLink_IsInPlay_CallsReplaceOrAddQueryStringParamsFuncWithCorrectRouteValues()
        {
            // Arrange
            var matchId = 1;
            query.Type = EventTypeConstants.InPlay;
            query.Market = MarketConstants.InPlay;

            var eventViewModel = new EventViewModel(
                query,
                string.Empty,
                new MatchLeagueTotal(query.Type, Enumerable.Empty<MatchLeague>(), 0, 0),
                Enumerable.Empty<SportGroupSummary>(),
                Enumerable.Empty<SelectListItem>(),
                Substitute.For<BrowserHelper>());

            var expectedRouteValues = new Dictionary<string, object>
            {
                ["type"] = query.Type,
                [nameof(matchId)] = matchId,
                [nameof(query.Stake)] = query.Stake,
                [nameof(query.StakeCondition)] = query.StakeCondition,
                [nameof(query.SportTypeGroup)] = query.SportTypeGroup,
                [nameof(query.PossType)] = query.PossType,
                [nameof(query.LeagueId)] = query.LeagueId,
                [nameof(query.IsAutoRefresh)] = query.IsAutoRefresh,
                [nameof(query.Market)] = query.Market
            };

            // Act
            eventViewModel.BuildViewByBetTypeLink(query.Type, matchId);

            // Assert
            Site.Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc.Received().Invoke(
                "TotalBetsPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRouteValues, routeValues).AreEqual));
        }

        [Fact]
        public void BuildViewByBetTypeLink_IsEarly_CallsReplaceOrAddQueryStringParamsFuncWithCorrectRouteValues()
        {
            // Arrange
            var matchId = 1;
            query.Type = EventTypeConstants.Early;
            query.Market = MarketConstants.InPlay;

            var eventViewModel = new EventViewModel(
                query,
                string.Empty,
                new MatchLeagueTotal(query.Type, Enumerable.Empty<MatchLeague>(), 0, 0),
                Enumerable.Empty<SportGroupSummary>(),
                Enumerable.Empty<SelectListItem>(),
                Substitute.For<BrowserHelper>());

            Site.Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc = Substitute.For<Func<string, IDictionary<string, object>, string>>();
            var expectedRouteValues = new Dictionary<string, object>
            {
                ["type"] = query.Type,
                [nameof(matchId)] = matchId,
                [nameof(query.Stake)] = query.Stake,
                [nameof(query.StakeCondition)] = query.StakeCondition,
                [nameof(query.SportTypeGroup)] = query.SportTypeGroup,
                [nameof(query.PossType)] = query.PossType,
                [nameof(query.LeagueId)] = query.LeagueId,
                [nameof(query.IsAutoRefresh)] = query.IsAutoRefresh,
                ["IsShowKODate"] = true
            };

            // Act
            eventViewModel.BuildViewByBetTypeLink(query.Type, matchId);

            // Assert
            Site.Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc.Received().Invoke(
                "TotalBetsPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRouteValues, routeValues).AreEqual));
        }

        [Fact]
        public void BuildViewByBetTypeLink_IsNotEarlyOrNotInPlay_CallsReplaceOrAddQueryStringParamsFuncWithCorrectRouteValues()
        {
            // Arrange
            var matchId = 1;
            query.Type = EventTypeConstants.Today;
            query.Market = MarketConstants.InPlay;

            var eventViewModel = new EventViewModel(
                query,
                string.Empty,
                new MatchLeagueTotal(query.Type, Enumerable.Empty<MatchLeague>(), 0, 0),
                Enumerable.Empty<SportGroupSummary>(),
                Enumerable.Empty<SelectListItem>(),
                Substitute.For<BrowserHelper>());

            Site.Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc = Substitute.For<Func<string, IDictionary<string, object>, string>>();
            var expectedRouteValues = new Dictionary<string, object>
            {
                ["type"] = query.Type,
                [nameof(matchId)] = matchId,
                [nameof(query.Stake)] = query.Stake,
                [nameof(query.StakeCondition)] = query.StakeCondition,
                [nameof(query.SportTypeGroup)] = query.SportTypeGroup,
                [nameof(query.PossType)] = query.PossType,
                [nameof(query.LeagueId)] = query.LeagueId,
                [nameof(query.IsAutoRefresh)] = query.IsAutoRefresh,
                ["IsShowKODate"] = false
            };

            // Act
            eventViewModel.BuildViewByBetTypeLink(query.Type, matchId);

            // Assert
            Site.Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc.Received().Invoke(
                "TotalBetsPath",
                Arg.Compat.Is<Dictionary<string, object>>(
                    routeValues => new CompareLogic().Compare(expectedRouteValues, routeValues).AreEqual));
        }
    }
}