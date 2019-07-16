namespace CN88.TotalBets.Features.Summaries.Criteria
{
    using System;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Shared.Constants;
    using Shared.Models;

    public class GetSummaryByEventCriteria : CriteriaBase
    {
        public GetSummaryByEventCriteria(
            int custId,
            int subAccId,
            bool isAllDownline,
            byte eventType,
            UserLevel custLevel)
        {
            if (custLevel.IsMemberLevel())
            {
                throw new ArgumentOutOfRangeException(nameof(custLevel));
            }

            CustId = custId;
            SubAccId = subAccId;
            IsAllDownline = isAllDownline;
            EventType = eventType;
            CustLevel = custLevel;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsAllDownline { get; }

        [SpParam(SqlParamName = "Type")]
        public byte EventType { get; }

        [SpParam(Ignored = true)]
        public UserLevel CustLevel { get; }

        public override string GetSettingKey()
            => EventType == EventTypeConstants.Started
                ? $"CS_TotalBets_Summary_Started_{CustLevel.DisplayName}"
                : $"CS_TotalBets_Summary_{CustLevel.DisplayName}";

        public override bool IsValid() => CustId > 0;
    }
}