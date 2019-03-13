using CN88.TotalBets.Models;
using Fanex.Data;
using Fanex.Data.Repository;

namespace CN88.TotalBets.Criteria
{
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

        public string TypeName => "Overview";

        public override bool IsValid()
            => CustId > 0 && PossType >= 0;

        public override string GetSettingKey()
            => UserLevel.IsSuperLevel() ? "CS_TotalBets_Overview_Super" : UserLevel.IsMasterLevel() ?
            "CS_TotalBets_Overview_Master" : "CS_TotalBets_Overview_Agent";
    }
}
