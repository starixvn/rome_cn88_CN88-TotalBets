namespace CN88.TotalBets.Features.Summaries.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Shared.Constants;
    using Shared.Models;

    public class GetSummaryByEventCriteria : CriteriaBase
    {
        public GetSummaryByEventCriteria(
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
        {
            if (Type == EventTypeConstants.Started)
            {
                return CustLevel.IsAgentLevel()
                    ? "CS_TotalBets_Summary_Started_Agent"
                    : CustLevel.IsMasterLevel()
                    ? "CS_TotalBets_Summary_Started_Master"
                    : "CS_TotalBets_Summary_Started_Super";

            }
            return CustLevel.IsAgentLevel()
                ? "CS_TotalBets_Summary_Agent"
                : CustLevel.IsMasterLevel()
                ? "CS_TotalBets_Summary_Master"
                : "CS_TotalBets_Summary_Super";
        }

        public override bool IsValid() => CustId > 0;
    }
}