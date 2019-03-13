namespace CN88.TotalBets.Site.ViewModels.Outright
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Filters;
    using Requests.Outright;
    using TotalBets.Models.Outright;

    public class OutrightViewModel
    {
        private readonly OutrightQuery query;

        public OutrightViewModel(
            OutrightQuery query,
            IEnumerable<OutrightLeague> outrights,
            IEnumerable<SelectListItem> stakeConditions,
            IEnumerable<SelectListItem> leagues)
        {
            this.query = query;
            Outrights = outrights;
            StakeConditionFilterViewModel = new StakeConditionFilterViewModel(stakeConditions, query.StakeCondition, query.Stake);
            LeagueSelectionViewModel = new SingleSelectionViewModel(leagues, query.LeagueId);
        }

        public int PossType => query.PossType;

        public IEnumerable<OutrightLeague> Outrights { get; }

        public StakeConditionFilterViewModel StakeConditionFilterViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }
    }
}