using System.Threading;
using CN88.TotalBets.Shared.Services;
using Fanex.RemotingRef;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Shared.Services
{
    public class CacheRefServiceImplTests
    {
        private readonly IRemotingRefService remotingRefService;
        private readonly CacheRefServiceImpl cacheRefService;
        private readonly string languageCode;

        public CacheRefServiceImplTests()
        {
            remotingRefService = Substitute.For<IRemotingRefService>();
            cacheRefService = new CacheRefServiceImpl(remotingRefService);
            languageCode = Thread.CurrentThread.CurrentCulture.Name;
        }

        [Fact]
        public void GetLeagueName_Always_CallsGetLeagueName()
        {
            // Arrange
            var leagueId = 1;

            // Act
            cacheRefService.GetLeagueName(leagueId);

            // Assert
            remotingRefService.Received().GetLeagueName(leagueId.ToString(), languageCode);
        }

        [Fact]
        public void GetTeamName_Always_CallsGetTeamName()
        {
            // Arrange
            var teamId = 1;

            // Act
            cacheRefService.GetTeamName(teamId);

            // Assert
            remotingRefService.Received().GetTeamName(teamId.ToString(), languageCode);
        }

        [Fact]
        public void GetBetTypeName_Always_CallsGetBetTypeName()
        {
            // Arrange
            var betTypeId = 1;

            // Act
            cacheRefService.GetBetTypeName(betTypeId);

            // Assert
            remotingRefService.Received().GetBetTypeName(betTypeId.ToString(), languageCode);
        }

        [Fact]
        public void GetSportName_Always_CallsGetSportName()
        {
            // Arrange
            var sportTypeId = 1;

            // Act
            cacheRefService.GetSportName(sportTypeId);

            // Assert
            remotingRefService.Received().GetSportName(sportTypeId.ToString(), languageCode);
        }
    }
}