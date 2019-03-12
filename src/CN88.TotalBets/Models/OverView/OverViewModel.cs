using System.Collections.Generic;

namespace CN88.TotalBets.Models.OverView
{
    public class OverViewModel
    {
        public OverViewModel(
            IEnumerable<OverViewBySportTypeModel> sportTypeModels,
            OverViewByParlayModel parlayModel,
            OverViewItemModel totalOverView)
        {
            TotalOverView = totalOverView ?? new OverViewItemModel();
            BySportTypeModels = sportTypeModels;
            ParlayModel = parlayModel ?? new OverViewByParlayModel();
        }

        public OverViewItemModel TotalOverView { get; }

        public IEnumerable<OverViewBySportTypeModel> BySportTypeModels { get; }        

        public OverViewByParlayModel ParlayModel { get; }
    }
}
