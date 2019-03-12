namespace CN88.TotalBets.Site.ViewModels.Outright
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using Filters;
    using Requests.Outright;
    using TotalBets.Models.Outright;

    public class OutrightViewModel
    {
        private readonly OutrightQuery query;

        public OutrightViewModel(
            OutrightQuery query,
            IEnumerable<OutrightLeague> outrights,
            IEnumerable<SelectListItem> leagues)
        {
            this.query = query;
            Outrights = outrights;

            var stakeConditions = StakeConditionConstants.Conditions.Select(condition => new SelectListItem
            {
                Text = condition.Value,
                Value = condition.Key.ToString(),
                Selected = condition.Key == query.StakeCondition
            });
            StakeConditionFilterViewModel = new StakeConditionFilterViewModel(stakeConditions, query.StakeCondition, query.Stake);

            //var possTypes = PossTypeConstants.PossTypes.Select(type => new SelectListItem
            //{
            //    Text = type.Value,
            //    Value = type.Key.ToString(),
            //    Selected = type.Key == query.PossType
            //});
            //PossTypeSelectionViewModel = new SingleSelectionViewModel(possTypes, query.PossType);

            LeagueSelectionViewModel = new SingleSelectionViewModel(leagues, query.LeagueId);
        }

        public int PossType => query.PossType;

        public IEnumerable<OutrightLeague> Outrights { get; }

        public StakeConditionFilterViewModel StakeConditionFilterViewModel { get; }

        //public SingleSelectionViewModel PossTypeSelectionViewModel { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }
    }
}