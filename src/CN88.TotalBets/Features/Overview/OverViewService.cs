namespace CN88.TotalBets.Features.Overview
{
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;

    public interface IOverViewService
    {
        Task<OverViewModel> Get(int custId, int viewMode, byte roleId);
    }

    public class OverViewService : IOverViewService
    {
        private readonly IDynamicRepository dynamicRepository;

        public OverViewService(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<OverViewModel> Get(int custId, int viewMode, byte roleId)
        {
            var criteria = new GetOverViewCriteria(custId, viewMode, roleId);
            var overView = await dynamicRepository.FetchMultipleAsync<OverViewBySportTypeModel, OverViewByParlayModel, OverViewItemModel>(criteria);

            return new OverViewModel(overView.Item1, overView.Item2.FirstOrDefault(), overView.Item3.FirstOrDefault());
        }
    }
}