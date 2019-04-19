namespace CN88.TotalBets.Features.Summaries.Criteria
{
    using Shared.Models;
    using Shared.Constants;

    public class GetSummaryBySportTypeGroupCriteria : GetSummaryByEventCriteria
    {
        public GetSummaryBySportTypeGroupCriteria(
            int custId,
            byte type,
            byte sportTypeGroup,
            UserLevel custLevel)
            : base(custId, type, custLevel)
        {
            SportTypeGroup = sportTypeGroup;
        }

        public byte SportTypeGroup { get; }

        public override string GetSettingKey()
        {
            if (Type == EventTypeConstants.Started)
            {
                return CustLevel.IsAgentLevel()
                    ? "CS_TotalBets_Detail_Started_Agent"
                    : CustLevel.IsMasterLevel()
                    ? "CS_TotalBets_Detail_Started_Master"
                    : "CS_TotalBets_Detail_Started_Super";
            }

            return CustLevel.IsAgentLevel()
                ? "CS_TotalBets_Detail_Agent"
                : CustLevel.IsMasterLevel()
                ? "CS_TotalBets_Detail_Master"
                : "CS_TotalBets_Detail_Super";
        }
    }
}