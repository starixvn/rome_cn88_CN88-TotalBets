namespace CN88.TotalBets.Criteria
{
    using Fanex.Data.Repository;

    public class GetLeagueCriteria : CriteriaBase
    {
        public GetLeagueCriteria(int sportTypeGroup, string typeName)
        {
            SportTypeGroup = sportTypeGroup;
            TypeName = typeName;
        }

        public int SportTypeGroup { get; }

        public string TypeName { get; }

        public override string GetSettingKey() => "CS_TotalBets_League_Sel";

        public override bool IsValid() => true;
    }
}