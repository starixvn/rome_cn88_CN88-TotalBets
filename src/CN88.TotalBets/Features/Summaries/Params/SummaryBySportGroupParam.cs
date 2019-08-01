namespace CN88.TotalBets.Features.Summaries.Params
{
    using Shared.Models;

    public class SummaryBySportGroupParam : SummaryByEventParam
    {
        public SummaryBySportGroupParam(
            int custId,
            int subAccId,
            bool isAllDownline,
            byte eventType,
            byte sportGroup,
            UserLevel custLevel)
            : base(custId, subAccId, isAllDownline, eventType, custLevel)
        {
            SportGroup = sportGroup;
        }

        public byte SportGroup { get; }
    }
}