using System;
using System.Collections.Generic;
using System.Linq;
using CN88.TotalBets.Constants;
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
                getSportDisplayName(SportTypeConstants.Soccer),
                getSportDisplayName(SportTypeConstants.Basketball),
                getSportDisplayName(SportTypeConstants.Tennis),
                getSportDisplayName(SportTypeConstants.Volleyball),
                getSportDisplayName(SportTypeConstants.TableTennis),
                getSportDisplayName(SportTypeConstants.Badminton),
                getSportDisplayName(SportTypeConstants.Baseball),
                getSportDisplayName(SportTypeConstants.Snooker).Split('/')[0],
                getSportDisplayName(SportTypeConstants.OtherSport)
            };
        }

        public List<string> HeaderName { get; }

        public List<string> ToolTip { get; }
    }
}