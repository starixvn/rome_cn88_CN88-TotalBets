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
        {
            Soccer = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 1).InplayWager, model.FirstOrDefault(i => i.SportTypeGroup == 1).InplayStake);
            Basketball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 2).InplayWager, model.FirstOrDefault(i => i.SportTypeGroup == 2).InplayStake);
            Tennis = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 3).InplayWager, model.FirstOrDefault(i => i.SportTypeGroup == 3).InplayStake);
            Volleyball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 4).InplayWager, model.FirstOrDefault(i => i.SportTypeGroup == 4).InplayStake);
            TableTennis = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 7).InplayWager, model.FirstOrDefault(i => i.SportTypeGroup == 7).InplayStake);
            Badminton = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 6).InplayWager, model.FirstOrDefault(i => i.SportTypeGroup == 6).InplayStake);
            Baseball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 5).InplayWager, model.FirstOrDefault(i => i.SportTypeGroup == 5).InplayStake);
            Snooker = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 8).InplayWager, model.FirstOrDefault(i => i.SportTypeGroup == 8).InplayStake);
            OtherSports = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 99).InplayWager, model.FirstOrDefault(i => i.SportTypeGroup == 99).InplayStake);
        }

        public override string Text => "In-Play";

        public override string Url(int possType, decimal stake)
            => UrlHelper.AddQueryStringParamsFunc(
                AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/Event/Index"),
                new Dictionary<string, object>
                {
                    ["PossType"] = possType,
                    ["Stake"] = stake,
                    ["Type"] = EventTypeConstants.InPlay
                });
    }
}