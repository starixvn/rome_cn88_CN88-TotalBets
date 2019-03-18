namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Web.Helpers;
    using Shared.Helpers;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Shared.Configurations;
    using TotalBets.Shared.Constants;

    public class OverViewByTodayViewModel : OverViewByBaseViewModel
    {
        public OverViewByTodayViewModel(IEnumerable<OverViewBySportTypeModel> model)
            : base(model)
        {
            Soccer = new OverViewItemModel(SoccerRow.SportTypeGroup, SoccerRow.TodayWager, SoccerRow.TodayStake);
            Basketball = new OverViewItemModel(BasketballRow.SportTypeGroup, BasketballRow.TodayWager, BasketballRow.TodayStake);
            Tennis = new OverViewItemModel(TennisRow.SportTypeGroup, TennisRow.TodayWager, TennisRow.TodayStake);
            Volleyball = new OverViewItemModel(VolleyballRow.SportTypeGroup, VolleyballRow.TodayWager, VolleyballRow.TodayStake);
            TableTennis = new OverViewItemModel(TableTennisRow.SportTypeGroup, TableTennisRow.TodayWager, TableTennisRow.TodayStake);
            Badminton = new OverViewItemModel(BadmintonRow.SportTypeGroup, BadmintonRow.TodayWager, BadmintonRow.TodayStake);
            Baseball = new OverViewItemModel(BaseballRow.SportTypeGroup, BaseballRow.TodayWager, BadmintonRow.TodayStake);
            Snooker = new OverViewItemModel(SnookerRow.SportTypeGroup, SnookerRow.TodayWager, SnookerRow.TodayStake);
            OtherSports = new OverViewItemModel(OtherSportsRow.SportTypeGroup, OtherSportsRow.TodayWager, OtherSportsRow.TodayStake);
        }

        public override string Text => "Today";

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