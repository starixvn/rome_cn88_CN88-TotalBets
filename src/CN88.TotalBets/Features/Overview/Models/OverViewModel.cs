using System.Collections.Generic;

namespace CN88.TotalBets.Models.OverView
{
    public class OverViewModel
    {
        public OverViewModel(
            IEnumerable<OverViewBySportTypeModel> bySportTypeModels,
            OverViewByParlayModel parlayModel,
            OverViewItemModel total)
        {
            Total = total ?? new OverViewItemModel();
            BySportTypeModels = bySportTypeModels;
            ParlayModel = parlayModel ?? new OverViewByParlayModel();
        }

        public OverViewItemModel Total { get; }

        public IEnumerable<OverViewBySportTypeModel> BySportTypeModels { get; }        

        public OverViewByParlayModel ParlayModel { get; }
    }
}
