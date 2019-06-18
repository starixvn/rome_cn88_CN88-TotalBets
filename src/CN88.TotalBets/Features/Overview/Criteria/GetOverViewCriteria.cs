namespace CN88.TotalBets.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Shared.Models;

    public class GetOverViewCriteria : CriteriaBase
    {
        public GetOverViewCriteria(int custId, int possType, byte roleId)
        {
            CustId = custId;
            PossType = possType;
            UserLevel = Enumeration.FromValue<UserLevel>(roleId);
        }

        [SpParam(Ignored = true)]
        public UserLevel UserLevel { get; }

        public int CustId { get; }

        public int PossType { get; }

        public override bool IsValid()
            => CustId > 0 && PossType >= 0;

        public override string GetSettingKey()
            => $"CS_TotalBets_Overview_{UserLevel.DisplayName}";
    }
}