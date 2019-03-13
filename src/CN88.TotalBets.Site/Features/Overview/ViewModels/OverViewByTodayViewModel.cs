using System.Collections.Generic;
using System.Linq;
using CN88.TotalBets.Models.OverView;

namespace CN88.TotalBets.Site.ViewModels
{
    public class OverViewByTodayViewModel : OverViewByBaseViewModel
    {
        public OverViewByTodayViewModel(IEnumerable<OverViewBySportTypeModel> model)
        {
            Soccer = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 1).TodayWager, model.FirstOrDefault(i => i.SportTypeGroup == 1).TodayStake);
            Basketball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 2).TodayWager, model.FirstOrDefault(i => i.SportTypeGroup == 2).TodayStake);
            Tennis = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 3).TodayWager, model.FirstOrDefault(i => i.SportTypeGroup == 3).TodayStake);
            Volleyball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 4).TodayWager, model.FirstOrDefault(i => i.SportTypeGroup == 4).TodayStake);
            TableTennis = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 7).TodayWager, model.FirstOrDefault(i => i.SportTypeGroup == 7).TodayStake);
            Badminton = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 6).TodayWager, model.FirstOrDefault(i => i.SportTypeGroup == 6).TodayStake);
            Baseball = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 5).TodayWager, model.FirstOrDefault(i => i.SportTypeGroup == 5).TodayStake);
            Snooker = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 8).TodayWager, model.FirstOrDefault(i => i.SportTypeGroup == 8).TodayStake);
            OtherSports = new OverViewItemModel(model.FirstOrDefault(i => i.SportTypeGroup == 99).TodayWager, model.FirstOrDefault(i => i.SportTypeGroup == 99).TodayStake);
        }

        public override string Text => "Today";
    }
}