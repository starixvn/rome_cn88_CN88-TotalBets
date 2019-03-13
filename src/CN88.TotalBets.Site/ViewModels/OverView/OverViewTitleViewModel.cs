using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CN88.TotalBets.Models;

namespace CN88.TotalBets.Site.ViewModels.OverView
{
    public class OverViewTitleViewModel
    {
        public OverViewTitleViewModel(
            IEnumerable<int> sportTypeGroups, 
            IEnumerable<SportGroup> allSportTypeGroup , 
            Func<int, string> getSportDisplayName)
        {            
            ToolTip = allSportTypeGroup.Where(i => sportTypeGroups.Contains(i.Id)).Select(i => i.DisplayName).ToList();
            HeaderName = new List<string>
            {
                getSportDisplayName(1),
                getSportDisplayName(2),
                getSportDisplayName(5),
                getSportDisplayName(6),
                getSportDisplayName(18),
                getSportDisplayName(9),
                getSportDisplayName(8),
                getSportDisplayName(7).Split('/')[0],
                getSportDisplayName(99)
            };
        }

        public List<string> HeaderName { get; }

        public List<string> ToolTip { get; }
    }
}