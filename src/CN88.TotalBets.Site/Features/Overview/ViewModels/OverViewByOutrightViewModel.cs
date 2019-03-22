namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Web.Helpers;
    using AppResources;
    using Shared.Helpers;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Shared.Configurations;

    public class OverViewByOutrightViewModel : OverViewByBaseViewModel
    {
        public OverViewByOutrightViewModel(IEnumerable<OverViewBySportTypeModel> model)
        {
            foreach (var row in model)
            {
                SportsColumn.Add(new OverViewItemModel(row.SportTypeGroup, row.OutrightWager, row.OutrightStake));
            }            
        }

        public override string Text => Resources.ResourceManager.GetString("Outright");

        public override string SubMenuId => "OUTRIGHT";

        public override string Url(int possType, int sportTypeGroup)
            => UrlHelper.AddQueryStringParamsFunc(
                AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/Outright/Index"),
                new Dictionary<string, object>
                {
                    ["PossType"] = possType,
                    ["SportTypeGroup"] = sportTypeGroup
                });
    }
}