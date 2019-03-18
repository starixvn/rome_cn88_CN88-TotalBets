using CN88.TotalBets.Shared.Constants;
using CN88.TotalBets.Shared.Services;

namespace CN88.TotalBets.Features.Events.Models
{
    public class Match
    {
        public Match()
        {
        }

        public Match(
            int leagueId,
            int matchId,
            string showTime,
            int homeId,
            int awayId,
            int liveHomeScore,
            int liveAwayScore,
            int wager,
            decimal stake,
            int betTypeCount,
            int sportTypeId,
            bool isNeutral,
            bool offerLive)
        {
            LeagueId = leagueId;
            MatchId = matchId;
            ShowTime = showTime;
            HomeId = homeId;
            AwayId = awayId;
            LiveHomeScore = liveHomeScore;
            LiveAwayScore = liveAwayScore;
            Wager = wager;
            Stake = stake;
            BetTypeCount = betTypeCount;
            SportTypeId = sportTypeId;
            IsNeutral = isNeutral;
            OfferLive = offerLive;
        }

        public int LeagueId { get; }

        public int MatchId { get; }

        public string ShowTime { get; }

        public int HomeId { get; }

        public string HomeName => 
            CacheRefManager.CacheRefInstance.GetTeamName(HomeId)
            + (IsNeutral ? " (N)" : string.Empty);

        public int AwayId { get; }

        public string AwayName => CacheRefManager.CacheRefInstance.GetTeamName(AwayId);

        public int LiveHomeScore { get; }

        public int LiveAwayScore { get; }

        public int Wager { get; }

        public decimal Stake { get; }

        public int BetTypeCount { get; }

        public int SportTypeId { get; }

        public bool IsNeutral { get; }

        public bool OfferLive { get; }

        public bool IsShowScore => 
            SportTypeId == SportTypeConstants.Soccer
            && OfferLive;
    }
}