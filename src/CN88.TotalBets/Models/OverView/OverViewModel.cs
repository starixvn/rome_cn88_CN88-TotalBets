using System.Collections.Generic;

namespace CN88.TotalBets.Models.OverView
{
    public class OverViewModel
    {
        public OverViewModel(
            IEnumerable<OverViewBySportTypeModel> sportTypeModels,
            OverViewByParlayModel parlayModel,
            TotalOverViewModel totalOverView)
        {
            TotalOverView = totalOverView ?? new TotalOverViewModel();
            BySportTypeModels = sportTypeModels;
            ParlayModel = parlayModel ?? new OverViewByParlayModel();
        }

        public TotalOverViewModel TotalOverView { get; }

        public IEnumerable<OverViewBySportTypeModel> BySportTypeModels { get; }

        public OverViewByParlayModel ParlayModel { get; }
    }
}
