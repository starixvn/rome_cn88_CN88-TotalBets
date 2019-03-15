namespace CN88.TotalBets.Features.Summaries.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class SportGroupSummary
    {
        public SportGroupSummary()
        {
        }

        public SportGroupSummary(
            byte sportTypeGroup,
            int wager,
            decimal stake)
        {
            SportTypeGroup = sportTypeGroup;
            Wager = wager;
            Stake = stake;
        }

        public SportGroupSummary(
            byte sportTypeGroup,
            string sportTypeGroupName,
            IEnumerable<LeagueSummary> leagueSummaries)
        {
            SportTypeGroup = sportTypeGroup;
            SportTypeGroupName = sportTypeGroupName;
            LeagueSummaries = leagueSummaries;
            Wager = leagueSummaries.Select(s => s.Wagers).Sum();
            Stake = leagueSummaries.Select(s => Stake).Sum();
        }

        public byte SportTypeGroup { get; }

        public string SportTypeGroupName { get; internal set; }

        public int Wager { get; }

        public decimal Stake { get; }

        public IEnumerable<LeagueSummary> LeagueSummaries { get; }
    }
}