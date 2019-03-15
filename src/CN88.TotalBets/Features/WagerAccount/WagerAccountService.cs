namespace CN88.TotalBets.Features.WagerAccount
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fanex.Data.Repository;

    public interface IWagerAccountService
    {
        Task<IEnumerable<WagerAccountModel>> Get(WagerAccountParam param);
    }

    public class WagerAccountService : IWagerAccountService
    {
        private readonly IDynamicRepository dynamicRepository;

        public WagerAccountService(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<WagerAccountModel>> Get(WagerAccountParam param)
            => await dynamicRepository.FetchAsync<WagerAccountModel>(new GetWagerAccountCriteria(param));
    }
}
