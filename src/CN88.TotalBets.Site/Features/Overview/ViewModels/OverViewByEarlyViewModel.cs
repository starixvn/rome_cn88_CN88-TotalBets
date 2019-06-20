namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using AppResources;
    using Shared.Helpers;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Shared.Constants;

    public class OverViewByEarlyViewModel : OverViewByBaseViewModel
    {
        public OverViewByEarlyViewModel(IEnumerable<OverViewBySportTypeModel> model)
        {
            foreach (var row in model)
            {
                SportsColumn.Add(new OverViewItemModel(row.SportTypeGroup, row.EarlyWager, row.EarlyStake));
            }
        }

        public override string Text => Resources.ResourceManager.GetString("Early");

        public override string SubMenuId => "EARLY";

        public override string Url(int possType, int sportTypeGroup)
            => UrlHelper.AddQueryStringParamsFunc(
                UrlHelper.MapTotalBetsPath("~/Event/Index"),
                new Dictionary<string, object>
                {
                    ["PossType"] = possType,
                    ["SportTypeGroup"] = sportTypeGroup,
                    ["Type"] = EventTypeConstants.Early
                });
    }
}