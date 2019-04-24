namespace CN88.TotalBets.Features.Started
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using Shared.Services;

    public interface StartedService
    {
        Task<IEnumerable<StartedLeague>> GetStarted(StartedParam param);
    }

    public class StartedServiceImpl : StartedService
    {
        private readonly CacheRefService cacheRefService;
        private readonly IDynamicRepository dynamicRepository;

        public StartedServiceImpl(
            CacheRefService cacheRefService,
            IDynamicRepository dynamicRepository)
        {
            this.cacheRefService = cacheRefService;
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<StartedLeague>> GetStarted(StartedParam param)
        {
            var data = await dynamicRepository.FetchMultipleAsync<StartedMatch, StartedMatchSummary>(new GetStartedCriteria(param));

            if (data == null || !data.Item1.Any())
            {
                return Enumerable.Empty<StartedLeague>();
            }

            var matches = data.Item1;
            var matchSummaries = data.Item2;

            foreach (var match in matches)
            {
                match.HomeName = cacheRefService.GetTeamName(match.HomeId) + (match.IsNeutral ? " (N)" : string.Empty);
                match.AwayName = cacheRefService.GetTeamName(match.AwayId);
                var summaries = matchSummaries.Where(s => s.MatchId == match.MatchId);

                var preMatchSummary = summaries.FirstOrDefault(s => !s.LiveIndicator);
                if (preMatchSummary != null)
                {
                    match.PMWager = preMatchSummary.Wager;
                    match.PMStake = preMatchSummary.Stake;
                }

                var inPlaySummary = summaries.FirstOrDefault(s => s.LiveIndicator);
                if (inPlaySummary != null)
                {
                    match.IPWager = inPlaySummary.Wager;
                    match.IPStake = inPlaySummary.Stake;
                }
            }

            return matches
                .GroupBy(match => match.LeagueId)
                .Select(group => new StartedLeague(
                    group.Key,
                    cacheRefService.GetLeagueName(group.Key),
                    group.OrderByDescending(x => x.TotalStake)))
                .OrderBy(x => x.LeagueName);
        }
    }
}