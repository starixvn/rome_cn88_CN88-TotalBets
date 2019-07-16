namespace CN88.TotalBets.Features.Overview.Params
{
    using Shared.Models;

    public class OverViewParam
    {
        public OverViewParam(
            int custId,
            int subAccId,
            bool isAllDownline,
            int possType,
            UserLevel userLevel)
        {
            CustId = custId;
            SubAccId = subAccId;
            IsAllDownline = isAllDownline;
            PossType = possType;
            UserLevel = userLevel;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsAllDownline { get; }

        public int PossType { get; }

        public UserLevel UserLevel { get; }
    }
}