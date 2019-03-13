namespace CN88.TotalBets.Features.Summay.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Shared.Models;

    public class GetSummaryCriteria : CriteriaBase
    {
        public GetSummaryCriteria(
            int custId,
            byte type,
            UserLevel custLevel)
        {
            CustId = custId;
            Type = type;
            CustLevel = custLevel;
        }

        public int CustId { get; }

        public byte Type { get; }

        [SpParam(Ignored = true)]
        public UserLevel CustLevel { get; }

        public override string GetSettingKey()
            => CustLevel.IsAgentLevel()
                ? "CS_TotalBets_Summary_Agent"
                : CustLevel.IsMasterLevel()
                ? "CS_TotalBets_Summary_Master"
                : "CS_TotalBets_Summary_Super";

        public override bool IsValid() => CustId > 0;
    }
}