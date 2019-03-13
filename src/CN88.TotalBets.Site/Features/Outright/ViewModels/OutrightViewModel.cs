namespace CN88.TotalBets.Site.Features.Outright.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Outright;
    using Shared.ViewModels.Filters;
    using TotalBets.Features.Outright.Models;

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
            LeagueSelectionViewModel = new SingleSelectionViewModel(leagues, query.LeagueId);
        }

        public int PossType => query.PossType;

        public IEnumerable<OutrightLeague> Outrights { get; }

        public SingleSelectionViewModel LeagueSelectionViewModel { get; }

        public FilterBaseViewModel FilterViewModel
            => new FilterBaseViewModel(
                query.StakeCondition,
                query.Stake,
                query.PossType,
                LeagueSelectionViewModel);
    }
}