namespace CN88.TotalBets.Site.ViewModels.OverView
{
    using System.Collections.Generic;
    using System.Linq;
    using TotalBets.Features.SportTypeGroup.Models;

    public class OverViewTitleViewModel
    {
        public OverViewTitleViewModel(
            IEnumerable<int> sportTypeGroups,
            IEnumerable<SportGroup> allSportTypeGroup)
        {
            ToolTip = allSportTypeGroup.Where(i => sportTypeGroups.Contains(i.Id)).Select(i => i.DisplayName).ToList();
        }

        public List<string> HeaderName
        {
            get
            {
                var headerNames = new List<string>();
                foreach (var columnName in ToolTip)
                {
                    var firstSport = columnName.Split('/');
                    headerNames.Add(firstSport.Count() > 1 ? firstSport[0] : columnName);
                }

                return headerNames;
            }
        }

        public List<string> ToolTip { get; }
    }
}