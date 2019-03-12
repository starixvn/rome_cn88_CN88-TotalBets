using System.Linq;
using CN88.TotalBets.Criteria;
using CN88.TotalBets.Models.OverView;
using Fanex.Data.Repository;

namespace CN88.TotalBets.Services
{
    public interface IOverViewService
    {
        OverViewModel Get(int custId, int viewMode, byte roleId);
    }

    public class OverViewService : IOverViewService
    {
        private readonly IDynamicRepository dynamicRepository;

        public OverViewService(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        } 

        public OverViewModel Get(int custId, int viewMode, byte roleId)
        {
            var criteria = new GetOverViewCriteria(custId, viewMode, roleId);
            var overView = dynamicRepository.FetchMultiple<OverViewBySportTypeModel, OverViewByParlayModel, OverViewItemModel>(criteria);

            return new OverViewModel(overView.Item1, overView.Item2.FirstOrDefault(), overView.Item3.FirstOrDefault());
        }
    }
}
