namespace CN88.TotalBets.Features.Summaries.Params
{
    using Shared.Models;

    public class SummaryByEventParam
    {
        public SummaryByEventParam(
            int custId,
            int subAccId,
            bool isAllDownline,
            byte eventType,
            UserLevel custLevel)
        {
            CustId = custId;
            SubAccId = subAccId;
            IsAllDownline = isAllDownline;
            EventType = eventType;
            CustLevel = custLevel;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsAllDownline { get; }

        public byte EventType { get; }

        public UserLevel CustLevel { get; }
    }
}