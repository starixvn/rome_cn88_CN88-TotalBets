namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Web.Helpers;
    using AppResources;
    using Shared.Helpers;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Shared.Configurations;
    using TotalBets.Shared.Constants;

    public class OverViewByTodayViewModel : OverViewByBaseViewModel
    {
        public OverViewByTodayViewModel(IEnumerable<OverViewBySportTypeModel> model)
        {
            foreach (var row in model)
            {
                SportsColumn.Add(new OverViewItemModel(row.SportTypeGroup, row.TodayWager, row.TodayStake));
            }
        }

        public override string Text => Resources.ResourceManager.GetString("Today");

        public override string SubMenuId => "TODAY";

        public override string Url(int possType, int sportTypeGroup)
            => UrlHelper.AddQueryStringParamsFunc(
                AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/Event/Index"),
                new Dictionary<string, object>
                {
                    ["PossType"] = possType,
                    ["SportTypeGroup"] = sportTypeGroup,
                    ["Type"] = EventTypeConstants.Today
                });
    }
}