namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Web.Helpers;
    using Shared.Helpers;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Shared.Configurations;
    using TotalBets.Shared.Constants;

    public class OverViewByEarlyViewModel : OverViewByBaseViewModel
    {
        public OverViewByEarlyViewModel(IEnumerable<OverViewBySportTypeModel> model)
            : base(model)
        {
            Soccer = new OverViewItemModel(SoccerRow.SportTypeGroup, SoccerRow.EarlyWager, SoccerRow.EarlyStake);
            Basketball = new OverViewItemModel(BasketballRow.SportTypeGroup, BasketballRow.EarlyWager, BasketballRow.EarlyStake);
            Tennis = new OverViewItemModel(TennisRow.SportTypeGroup, TennisRow.EarlyWager, TennisRow.EarlyStake);
            Volleyball = new OverViewItemModel(VolleyballRow.SportTypeGroup, VolleyballRow.EarlyWager, VolleyballRow.EarlyStake);
            TableTennis = new OverViewItemModel(TableTennisRow.SportTypeGroup, TableTennisRow.EarlyWager, TableTennisRow.EarlyStake);
            Badminton = new OverViewItemModel(BadmintonRow.SportTypeGroup, BadmintonRow.EarlyWager, BadmintonRow.EarlyStake);
            Baseball = new OverViewItemModel(BaseballRow.SportTypeGroup, BaseballRow.EarlyWager, BadmintonRow.EarlyStake);
            Snooker = new OverViewItemModel(SnookerRow.SportTypeGroup, SnookerRow.EarlyWager, SnookerRow.EarlyStake);
            OtherSports = new OverViewItemModel(OtherSportsRow.SportTypeGroup, OtherSportsRow.EarlyWager, OtherSportsRow.EarlyStake);
        }

        public override string Text => "Early";

        public override string Url(int possType, int sportTypeGroup)
            => UrlHelper.AddQueryStringParamsFunc(
                AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/Event/Index"),
                new Dictionary<string, object>
                {
                    ["PossType"] = possType,
                    ["SportTypeGroup"] = sportTypeGroup,
                    ["Type"] = EventTypeConstants.Early
                });
    }
}