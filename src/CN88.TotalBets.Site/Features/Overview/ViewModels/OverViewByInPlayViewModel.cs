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
            var soccerRow = model.FirstOrDefault(i => i.SportTypeGroup == 1);
            var basketballRow = model.FirstOrDefault(i => i.SportTypeGroup == 2);
            var tennisRow = model.FirstOrDefault(i => i.SportTypeGroup == 3);
            var volleyballRow = model.FirstOrDefault(i => i.SportTypeGroup == 4);
            var tableTennisRow = model.FirstOrDefault(i => i.SportTypeGroup == 7);
            var badmintonRow = model.FirstOrDefault(i => i.SportTypeGroup == 6);
            var baseballRow = model.FirstOrDefault(i => i.SportTypeGroup == 5);
            var snookerRow = model.FirstOrDefault(i => i.SportTypeGroup == 8);
            var otherSportsRow = model.FirstOrDefault(i => i.SportTypeGroup == 99);
            Soccer = new OverViewItemModel(soccerRow.SportTypeGroup, soccerRow.InplayWager, soccerRow.InplayStake);
            Basketball = new OverViewItemModel(basketballRow.SportTypeGroup, basketballRow.InplayWager, basketballRow.InplayStake);
            Tennis = new OverViewItemModel(tennisRow.SportTypeGroup, tennisRow.InplayWager, tennisRow.InplayStake);
            Volleyball = new OverViewItemModel(volleyballRow.SportTypeGroup, volleyballRow.InplayWager, volleyballRow.InplayStake);
            TableTennis = new OverViewItemModel(tableTennisRow.SportTypeGroup, tableTennisRow.InplayWager, tableTennisRow.InplayStake);
            Badminton = new OverViewItemModel(badmintonRow.SportTypeGroup, badmintonRow.InplayWager, badmintonRow.InplayStake);
            Baseball = new OverViewItemModel(baseballRow.SportTypeGroup, baseballRow.InplayWager, baseballRow.InplayStake);
            Snooker = new OverViewItemModel(snookerRow.SportTypeGroup, snookerRow.InplayWager, snookerRow.InplayStake);
            OtherSports = new OverViewItemModel(otherSportsRow.SportTypeGroup, otherSportsRow.InplayWager, otherSportsRow.InplayStake);
        }

        public override string Text => "In-Play";

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