namespace CN88.TotalBets.Features.Summaries.Criteria
{
    using Shared.Constants;
    using Shared.Models;

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
             => EventType == EventTypeConstants.Started
                ? $"CS_TotalBets_Detail_Started_{CustLevel.DisplayName}"
                : $"CS_TotalBets_Detail_{CustLevel.DisplayName}";
    }
}