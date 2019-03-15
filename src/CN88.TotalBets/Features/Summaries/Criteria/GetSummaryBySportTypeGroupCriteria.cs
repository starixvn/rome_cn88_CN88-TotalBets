namespace CN88.TotalBets.Features.Summaries.Criteria
{
    using CN88.TotalBets.Shared.Models;

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
            => CustLevel.IsAgentLevel()
                ? "CS_TotalBets_Detail_Agent"
                : CustLevel.IsMasterLevel()
                ? "CS_TotalBets_Detail_Master"
                : "CS_TotalBets_Detail_Super";
    }
}