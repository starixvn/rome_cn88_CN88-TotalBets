namespace CN88.TotalBets.Features.Started.Models
{
    using System;

    public class StartedMatch
    {
        public StartedMatch()
        {
        }

        public StartedMatch(
            int leagueId,
            int matchId,
            int homeId,
            int awayId,
            int liveHomeScore,
            int liveAwayScore,
            int htHomeScore,
            int htAwayScore,
            DateTime showTimeDT,
            bool offerLive,
            bool isNeutral,
            bool liveType,
            int betTypeCount)
        {
            LeagueId = leagueId;
            MatchId = matchId;
            HomeId = homeId;
            AwayId = awayId;
            LiveHomeScore = liveHomeScore;
            LiveAwayScore = liveAwayScore;
            HTHomeScore = htHomeScore;
            HTAwayScore = htAwayScore;
            ShowTimeDT = showTimeDT;
            OfferLive = offerLive;
            IsNeutral = isNeutral;
            LiveType = liveType;
            BetTypeCount = betTypeCount;
        }

        public int LeagueId { get; }

        public int MatchId { get; }

        public int HomeId { get; }

        public string HomeName { get; internal set; }

        public int AwayId { get; }

        public string AwayName { get; internal set; }

        public int LiveHomeScore { get; }

        public int LiveAwayScore { get; }

        public int HTHomeScore { get; }

        public int HTAwayScore { get; }

        public DateTime ShowTimeDT { get; }

        public bool OfferLive { get; }

        public bool IsNeutral { get; }

        public bool LiveType { get; }

        public int BetTypeCount { get; }

        public int PMWager { get; internal set; }

        public decimal PMStake { get; internal set; }

        public int IPWager { get; internal set; }

        public decimal IPStake { get; internal set; }

        public decimal TotalStake => PMStake + IPStake;
    }
}