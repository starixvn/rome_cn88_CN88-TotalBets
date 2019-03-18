namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Web.Helpers;
    using Shared.Helpers;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Shared.Configurations;
    using TotalBets.Shared.Constants;

    public class OverViewByOutrightViewModel : OverViewByBaseViewModel
    {
        public OverViewByOutrightViewModel(IEnumerable<OverViewBySportTypeModel> model)
        {
            Soccer = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 1).OutrightWager, model.FirstOrDefault(i => i.SportTypeGroup == 1).OutrightStake);
            Basketball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 2).OutrightWager, model.FirstOrDefault(i => i.SportTypeGroup == 2).OutrightStake);
            Tennis = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 3).OutrightWager, model.FirstOrDefault(i => i.SportTypeGroup == 3).OutrightStake);
            Volleyball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 4).OutrightWager, model.FirstOrDefault(i => i.SportTypeGroup == 4).OutrightStake);
            TableTennis = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 7).OutrightWager, model.FirstOrDefault(i => i.SportTypeGroup == 7).OutrightStake);
            Badminton = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 6).OutrightWager, model.FirstOrDefault(i => i.SportTypeGroup == 6).OutrightStake);
            Baseball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 5).OutrightWager, model.FirstOrDefault(i => i.SportTypeGroup == 5).OutrightStake);
            Snooker = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 8).OutrightWager, model.FirstOrDefault(i => i.SportTypeGroup == 8).OutrightStake);
            OtherSports = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 99).OutrightWager, model.FirstOrDefault(i => i.SportTypeGroup == 99).OutrightStake);
        }

        public override string Text => "Outright";

        public override string Url(int possType, decimal stake)
            => UrlHelper.AddQueryStringParamsFunc(
                AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/Outright/Index"),
                new Dictionary<string, object>
                {
                    ["PossType"] = possType,
                    ["Stake"] = stake
                });
    }
}