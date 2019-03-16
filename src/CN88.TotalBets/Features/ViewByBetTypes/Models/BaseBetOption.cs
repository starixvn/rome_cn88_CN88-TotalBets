namespace CN88.TotalBets.Features.ViewByBetTypes.Models
{
    public class BaseBetOption
    {
        public BaseBetOption()
        {
        }

        public BaseBetOption(
            int bettypeId,
            string betTeam,
            bool isLive,
            int wager,
            decimal stake)
        {
            BetTypeId = bettypeId;
#pragma warning disable RECS0021 // Warns about calls to virtual member functions occuring in the constructor
            BetTeam = betTeam;
#pragma warning restore RECS0021 // Warns about calls to virtual member functions occuring in the constructor
            IsLive = isLive;
            Wager = wager;
            Stake = stake;
        }

        public int BetTypeId { get; }

        public string BetTeam { get; }

        public string BetTeamText { get; internal set; }

        public bool IsLive { get; }

        public int Wager { get; }

        public decimal Stake { get; }
    }
}