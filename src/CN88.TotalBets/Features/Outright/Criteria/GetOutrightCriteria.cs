namespace CN88.TotalBets.Features.Outright.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Shared.Models;

    public class GetOutrightCriteria : CriteriaBase
    {
        public GetOutrightCriteria(
            int custId,
            UserLevel userLevel,
            int sportTypeGroup,
            int leagueId,
            byte stakeCondition,
            decimal stake,
            byte possType)
        {
            CustId = custId;
            UserLevel = userLevel;
            SportTypeGroup = sportTypeGroup;
            LeagueId = leagueId;
            StakeCondition = stakeCondition;
            Stake = stake;
            PossType = possType;
        }

        public int CustId { get; }

        public int SportTypeGroup { get; }

        public int LeagueId { get; }

        public byte StakeCondition { get; }

        public decimal Stake { get; }

        public byte PossType { get; }

        [SpParam(Ignored = true)]
        public UserLevel UserLevel { get; }

        public override string GetSettingKey()
            => UserLevel.IsSuperLevel()
            ? "CS_TotalBets_Outright_Super"
            : (UserLevel.IsMasterLevel() ? "CS_TotalBets_Outright_Master" : "CS_TotalBets_Outright_Agent");

        public override bool IsValid()
            => CustId > 0;
    }
}