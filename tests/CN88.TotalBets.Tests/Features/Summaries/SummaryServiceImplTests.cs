using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CN88.TotalBets.Features.SportTypeGroup;
using CN88.TotalBets.Features.Summaries;
using CN88.TotalBets.Features.Summaries.Criteria;
using CN88.TotalBets.Features.Summaries.Models;
using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Services;
using Fanex.Data.Repository;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Summaries
{
    public class SummaryServiceImplTests
    {
        private readonly Fixture fixture;
        private readonly CompareLogic compareLogic;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;
        private readonly SportTypeGroupService sportTypeGroupService;
        private readonly SummaryServiceImpl summaryService;

        public SummaryServiceImplTests()
        {
            fixture = new Fixture();
            compareLogic = new CompareLogic();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheRefService = Substitute.For<CacheRefService>();
            sportTypeGroupService = Substitute.For<SportTypeGroupService>();
            summaryService = new SummaryServiceImpl(dynamicRepository, cacheRefService, sportTypeGroupService);
        }

        [Fact]
        public void GetSportGroupSummaries_Always_CallsDynamicRepositoryFetchAsync()
        {
            // Arrange
            var custId = Util.RandomValidCustId(fixture);
            var custLevel = Util.RandomSMALevel(fixture);
            byte eventType = EventTypeConstants.Early;

            // Act
            summaryService.GetSportGroupSummaries(custId, custLevel, eventType).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().FetchAsync<SportGroupSummary>(
                Arg.Compat.Is<GetSummaryByEventCriteria>(
                    x => x.CustId == custId
                        && x.CustLevel == custLevel
                        && x.EventType == eventType));
        }

        [Fact]
        public void GetSportGroupSummary_Always_CallsCallsDynamicRepositoryFetchAsync()
        {
            // Arrange
            var custId = Util.RandomValidCustId(fixture);
            var custLevel = Util.RandomSMALevel(fixture);
            byte eventType = EventTypeConstants.Early;
            var sportGroup = fixture.Create<Generator<byte>>().First(x => x > 0);

            // Act
            summaryService.GetSportGroupSummary(custId, custLevel, eventType, sportGroup).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().FetchAsync<SummaryByMatchDto>(
                Arg.Compat.Is<GetSummaryBySportTypeGroupCriteria>(
                    x => x.CustId == custId
                        && x.CustLevel == custLevel
                        && x.EventType == eventType
                        && x.SportTypeGroup == sportGroup));
        }

        [Fact]
        public void GetSportGroupSummary_NoDataReturnFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var custId = Util.RandomValidCustId(fixture);
            var custLevel = Util.RandomSMALevel(fixture);
            byte eventType = EventTypeConstants.Early;
            dynamicRepository.FetchAsync<SportGroupSummary>(Arg.Compat.Any<GetSummaryByEventCriteria>())
                .Returns(Enumerable.Empty<SportGroupSummary>());
            var expectedResult = Enumerable.Empty<SportGroupSummary>();

            // Act
            var actualResult = summaryService.GetSportGroupSummaries(custId, custLevel, eventType)
                .GetAwaiter().GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetSportGroupSummary_HasDataReturnFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var custId = Util.RandomValidCustId(fixture);
            var custLevel = Util.RandomSMALevel(fixture);
            byte eventType = EventTypeConstants.Early;
            var summaries = new List<SportGroupSummary>
            {
                new SportGroupSummary(1,10,10000)
            };
            dynamicRepository.FetchAsync<SportGroupSummary>(Arg.Compat.Any<GetSummaryByEventCriteria>())
                .Returns(summaries);
            sportTypeGroupService.GetSportTypeGroupName(Arg.Compat.Any<int>())
                .Returns(string.Empty);

            // Act
            var actualResult = summaryService.GetSportGroupSummaries(custId, custLevel, eventType)
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(summaries, actualResult).AreEqual);
        }

        [Fact]
        public void GetSportGroupSummary_NoDataReturnsFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var custId = Util.RandomValidCustId(fixture);
            var custLevel = Util.RandomSMALevel(fixture);
            byte eventType = EventTypeConstants.Early;
            var sportGroup = fixture.Create<Generator<byte>>().First(x => x > 0);
            dynamicRepository.FetchAsync<SummaryByMatchDto>(Arg.Compat.Any<GetSummaryBySportTypeGroupCriteria>())
                .Returns(Enumerable.Empty<SummaryByMatchDto>());
            var expectedResult = new SportGroupSummary(0, string.Empty, Enumerable.Empty<LeagueSummary>());

            // Act
            var actualResult = summaryService.GetSportGroupSummary(custId, custLevel, eventType, sportGroup)
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void GetSportGroupSummary_HasDataReturnsFromDb_ReturnsCorrectResult()
        {
            // Arrange
            var custId = Util.RandomValidCustId(fixture);
            var custLevel = Util.RandomSMALevel(fixture);
            byte eventType = EventTypeConstants.Early;
            var sportGroup = fixture.Create<Generator<byte>>().First(x => x > 0);

            var dtos = new List<SummaryByMatchDto>
            {
                new SummaryByMatchDto( 1, 1, 1, 2, 5, 5000, true),
                new SummaryByMatchDto( 1, 2, 3, 4, 5, 5000, false)
            };
            dynamicRepository.FetchAsync<SummaryByMatchDto>(Arg.Compat.Any<GetSummaryBySportTypeGroupCriteria>())
                .Returns(dtos);

            cacheRefService.GetTeamName(Arg.Compat.Any<int>()).Returns("TeamName");
            cacheRefService.GetLeagueName(Arg.Compat.Any<int>()).Returns("LeagueName");
            sportTypeGroupService.GetSportTypeGroupName(sportGroup).Returns("SportTypeGroupName");
            var matchSummaries = new List<MatchSummary> {
                new MatchSummary(1,1,"TeamName (N)",2,"TeamName",5,5000),
                new MatchSummary(1,3,"TeamName",4,"TeamName",5,5000)
            };
            var leagueSummaries = new List<LeagueSummary>
            {
                new LeagueSummary(1,"LeagueName", matchSummaries.OrderByDescending(x=>x.Stake))
            };
            var expectedResult = new SportGroupSummary(sportGroup, "SportTypeGroupName", leagueSummaries.OrderBy(item => item.LeagueName));

            // Act
            var actualResult = summaryService.GetSportGroupSummary(custId, custLevel, eventType, sportGroup)
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }
    }
}