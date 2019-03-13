namespace CN88.TotalBets.Features.Leagues.Criteria
{
    using Fanex.Data.Repository;

    public class GetLeagueCriteria : CriteriaBase
    {
        public GetLeagueCriteria(int sportTypeGroup, int type)
        {
            SportTypeGroup = sportTypeGroup;
            Type = type;
        }

        public int SportTypeGroup { get; }

        public int Type { get; }

        public override string GetSettingKey() => "CS_TotalBets_League_Sel";

        public override bool IsValid() => true;
    }
}