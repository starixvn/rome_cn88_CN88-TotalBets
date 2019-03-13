namespace CN88.TotalBets.Criteria
{
    using Features.Outright.Criteria;
    using Shared.Models;

    public class GetInPlayCriteria : GetOutrightCriteria
    {
        public GetInPlayCriteria(
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

        public override string GetSettingKey()
            => UserLevel.IsSuperLevel()
            ? "CS_TotalBets_Event_Super"
            : (UserLevel.IsMasterLevel() ? "CS_TotalBets_Event_Master" : "CS_TotalBets_Event_Agent");
    }
}
