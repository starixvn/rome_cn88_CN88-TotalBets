namespace CN88.TotalBets.Features.Overview
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using Shared.Configurations;

    public interface IOverViewService
    {
        Task<OverViewModel> Get(OverViewParam param);
    }

    public class OverViewService : IOverViewService
    {
        private readonly IDynamicRepository dynamicRepository;

        public OverViewService(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<OverViewModel> Get(OverViewParam param)
        {
            var criteria = new GetOverViewCriteria(
                param.CustId,
                param.SubAccId,
                param.IsAllDownline,
                param.PossType,
                param.UserLevel);
            var overView = await dynamicRepository.FetchMultipleAsync<OverViewBySportTypeModel, OverViewByParlayModel, OverViewItemModel>(criteria);
            var overViewBySportType = OrderBy(overView.Item1);

            return new OverViewModel(overViewBySportType, overView.Item2.FirstOrDefault(), overView.Item3.FirstOrDefault());
        }

        private static IEnumerable<OverViewBySportTypeModel> OrderBy(IEnumerable<OverViewBySportTypeModel> list)
        {
            var orderBySportTypeGroupInOverviewPage = AppSettings.OrderBySportTypeGroupInOverviewPage;
            var sportTypeHasConfigurate = list.Where(i => orderBySportTypeGroupInOverviewPage.ContainsKey(i.SportTypeGroup)).OrderBy(i => orderBySportTypeGroupInOverviewPage[i.SportTypeGroup]).ToList();
            var newSportType = list.Where(i => !orderBySportTypeGroupInOverviewPage.ContainsKey(i.SportTypeGroup));

            sportTypeHasConfigurate.AddRange(newSportType);
            return sportTypeHasConfigurate;
        }
    }
}