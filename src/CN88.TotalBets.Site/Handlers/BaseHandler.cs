namespace CN88.TotalBets.Site.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using TotalBets.Models;

    public class BaseHandler
    {
        protected internal IEnumerable<SelectListItem> BuildLeagueSelectListItems(IEnumerable<League> leagues, int selectedLeagueId)
        {
            var items = leagues.Select(league => new SelectListItem
            {
                Text = league.Name,
                Value = league.Id.ToString(),
                Selected = league.Id == selectedLeagueId
            }).ToList();

            items.Insert(0, new SelectListItem { Text = Resources.AllLeagues, Value = "0", Selected = selectedLeagueId == 0 });

            return items;
        }
    }
}