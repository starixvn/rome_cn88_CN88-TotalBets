namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Web.Helpers;
    using Shared.Helpers;
    using TotalBets.Features.Overview.Models;
    using TotalBets.Shared.Configurations;
    using TotalBets.Shared.Constants;

    public class OverViewByInPlayViewModel : OverViewByBaseViewModel
    {
        public OverViewByInPlayViewModel(IEnumerable<OverViewBySportTypeModel> model)
            : base(model)
        {            
            Soccer = new OverViewItemModel(SoccerRow.SportTypeGroup, SoccerRow.InplayWager, SoccerRow.InplayStake);
            Basketball = new OverViewItemModel(BasketballRow.SportTypeGroup, BasketballRow.InplayWager, BasketballRow.InplayStake);
            Tennis = new OverViewItemModel(TennisRow.SportTypeGroup, TennisRow.InplayWager, TennisRow.InplayStake);
            Volleyball = new OverViewItemModel(VolleyballRow.SportTypeGroup, VolleyballRow.InplayWager, VolleyballRow.InplayStake);
            TableTennis = new OverViewItemModel(TableTennisRow.SportTypeGroup, TableTennisRow.InplayWager, TableTennisRow.InplayStake);
            Badminton = new OverViewItemModel(BadmintonRow.SportTypeGroup, BadmintonRow.InplayWager, BadmintonRow.InplayStake);
            Baseball = new OverViewItemModel(BaseballRow.SportTypeGroup, BaseballRow.InplayWager, BaseballRow.InplayStake);
            Snooker = new OverViewItemModel(SnookerRow.SportTypeGroup, SnookerRow.InplayWager, SnookerRow.InplayStake);
            OtherSports = new OverViewItemModel(OtherSportsRow.SportTypeGroup, OtherSportsRow.InplayWager, OtherSportsRow.InplayStake);
        }

        public override string Text => "In-Play";

        public override string SubMenuId => "INPLAY";

        public override string Url(int possType, int sportTypeGroup)
            => UrlHelper.AddQueryStringParamsFunc(
                AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/Event/Index"),
                new Dictionary<string, object>
                {
                    ["PossType"] = possType,
                    ["SportTypeGroup"] = sportTypeGroup,
                    ["Type"] = EventTypeConstants.InPlay
                });
    }
}