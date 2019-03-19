namespace CN88.TotalBets.Features.ViewByBetTypes.Models
{
    public class BetOption
    {
        public BetOption()
        {
        }

        public BetOption(
            int bettypeId,
            string betTeam,
            bool isLive,
            int wager = 0,
            decimal stake = 0m)
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

        public int Wager { get; internal set; }

        public decimal Stake { get; internal set; }

        public override bool Equals(object obj)
        {
            var betOption = obj as BetOption;

            if (betOption == null)
            {
                return false;
            }

            return betOption.BetTypeId == BetTypeId
                && betOption.IsLive == IsLive
                && string.Compare(betOption.BetTeam, BetTeam, true) == 0;
        }

        public override int GetHashCode()
            => BetTypeId.GetHashCode()
                + IsLive.GetHashCode()
                + BetTeam.GetHashCode();
    }
}