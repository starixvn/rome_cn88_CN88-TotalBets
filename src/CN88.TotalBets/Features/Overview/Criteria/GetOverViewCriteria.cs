namespace CN88.TotalBets.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Shared.Models;

    public class GetOverViewCriteria : CriteriaBase
    {
        public GetOverViewCriteria(int custId, int subAccId, bool isAllDownline, int possType, UserLevel userLevel)
        {
            CustId = custId;
            SubAccId = subAccId;
            IsAllDownline = isAllDownline;
            PossType = possType;
            UserLevel = userLevel;
        }

        [SpParam(Ignored = true)]
        public UserLevel UserLevel { get; }

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsAllDownline { get; }

        public int PossType { get; }

        public override bool IsValid()
            => CustId > 0 && PossType >= 0;

        public override string GetSettingKey()
            => $"CS_TotalBets_Overview_{UserLevel.DisplayName}";
    }
}