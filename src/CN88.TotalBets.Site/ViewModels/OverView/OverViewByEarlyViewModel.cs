using System.Collections.Generic;
using System.Linq;
using CN88.TotalBets.Models.OverView;

namespace CN88.TotalBets.Site.ViewModels
{
    public class OverViewByEarlyViewModel : OverViewByBaseViewModel
    {
        public OverViewByEarlyViewModel(IEnumerable<OverViewBySportTypeModel> model)
        {
            Soccer = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 1).EarlyWager, model.FirstOrDefault(i => i.SportTypeGroup == 1).EarlyStake);
            Basketball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 2).EarlyWager, model.FirstOrDefault(i => i.SportTypeGroup == 2).EarlyStake);
            Tennis = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 3).EarlyWager, model.FirstOrDefault(i => i.SportTypeGroup == 3).EarlyStake);
            Volleyball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 4).EarlyWager, model.FirstOrDefault(i => i.SportTypeGroup == 4).EarlyStake);
            TableTennis = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 7).EarlyWager, model.FirstOrDefault(i => i.SportTypeGroup == 7).EarlyStake);
            Badminton = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 6).EarlyWager, model.FirstOrDefault(i => i.SportTypeGroup == 6).EarlyStake);
            Baseball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 5).EarlyWager, model.FirstOrDefault(i => i.SportTypeGroup == 5).EarlyStake);
            Snooker = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 8).EarlyWager, model.FirstOrDefault(i => i.SportTypeGroup == 8).EarlyStake);
            OtherSports = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 99).EarlyWager, model.FirstOrDefault(i => i.SportTypeGroup == 99).EarlyStake);
        }

        public override string Text => "Early";

    }
}