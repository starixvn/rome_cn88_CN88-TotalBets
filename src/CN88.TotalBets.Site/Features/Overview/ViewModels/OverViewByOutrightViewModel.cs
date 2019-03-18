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
            : base(model)
        {
            Soccer = new OverViewItemModel(SoccerRow.SportTypeGroup, SoccerRow.OutrightWager, SoccerRow.OutrightStake);
            Basketball = new OverViewItemModel(BasketballRow.SportTypeGroup, BasketballRow.OutrightWager, BasketballRow.OutrightStake);
            Tennis = new OverViewItemModel(TennisRow.SportTypeGroup, TennisRow.OutrightWager, TennisRow.OutrightStake);
            Volleyball = new OverViewItemModel(VolleyballRow.SportTypeGroup, VolleyballRow.OutrightWager, VolleyballRow.OutrightStake);
            TableTennis = new OverViewItemModel(TableTennisRow.SportTypeGroup, TableTennisRow.OutrightWager, TableTennisRow.OutrightStake);
            Badminton = new OverViewItemModel(BadmintonRow.SportTypeGroup, BadmintonRow.OutrightWager, BadmintonRow.OutrightStake);
            Baseball = new OverViewItemModel(BaseballRow.SportTypeGroup, BaseballRow.OutrightWager, BadmintonRow.OutrightStake);
            Snooker = new OverViewItemModel(SnookerRow.SportTypeGroup, SnookerRow.OutrightWager, SnookerRow.OutrightStake);
            OtherSports = new OverViewItemModel(OtherSportsRow.SportTypeGroup, OtherSportsRow.OutrightWager, OtherSportsRow.OutrightStake);
        }

        public override string Text => "Outright";

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