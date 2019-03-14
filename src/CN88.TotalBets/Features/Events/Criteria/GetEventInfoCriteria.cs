namespace CN88.TotalBets.Features.Events.Criteria
{
    using Fanex.Data.Repository;

    public class GetEventInfoCriteria : CriteriaBase
    {
        public GetEventInfoCriteria(int matchId)
        {
            MatchId = matchId;
        }

        public int MatchId { get; }

        public override string GetSettingKey()
            => "CS_TotalBets_EventInfo";

        public override bool IsValid()
            => MatchId > 0;
    }
}