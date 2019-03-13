namespace CN88.TotalBets.Params
{
    using Models;

    public class GetInPlayParam : GetOutrightParam
    {
        public GetInPlayParam(
            int custId,
            UserLevel userLevel,
            byte type,
            int sportTypeGroup,
            int leagueId,
            byte stakeCondition,
            decimal stake,
            byte possType,
            byte isLive) : base(custId, userLevel, sportTypeGroup, leagueId, stakeCondition, stake, possType)
        {
            Type = type;
            IsLive = isLive;
        }

        public byte Type { get; }

        public byte IsLive { get; }
    }
}
