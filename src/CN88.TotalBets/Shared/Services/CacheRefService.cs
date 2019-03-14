namespace CN88.TotalBets.Shared.Services
{
    using System.Threading;
    using Fanex.RemotingRef;

    public interface CacheRefService
    {
        string GetLeagueName(int leagueId);

        string GetTeamName(int teamId);

        string GetSportName(int sportTypeId);

        string GetBetTypeName(int betTypeId);
    }

    public static class CacheRefManager
    {
        private static CacheRefService remotingRefService;

        public static CacheRefService CacheRefInstance
        {
            get
            {
                if(remotingRefService == null)
                {
                    remotingRefService = new CacheRefServiceImpl(new RemotingRefService());
                }

                return remotingRefService;
            }

            internal set
            {
                remotingRefService = value;
            }
        }

    }

    public class CacheRefServiceImpl : CacheRefService
    {
        private readonly IRemotingRefService remotingRefService;

        public CacheRefServiceImpl(IRemotingRefService remotingRefService)
        {
            this.remotingRefService = remotingRefService;
        }

        public string GetLeagueName(int leagueId)
            => remotingRefService.GetLeagueName(leagueId.ToString(), LanguageCode);

        public string GetTeamName(int teamId)
            => remotingRefService.GetTeamName(teamId.ToString(), LanguageCode);

        public string GetBetTypeName(int betTypeId)
            => remotingRefService.GetBetTypeName(betTypeId.ToString(), LanguageCode);

        public string GetSportName(int sportTypeId)
            => remotingRefService.GetSportName(sportTypeId.ToString(), LanguageCode);

        private static string LanguageCode
            => Thread.CurrentThread.CurrentCulture.Name;
    }
}