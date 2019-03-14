namespace CN88.TotalBets.Features.InPlay
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models;
    using Params;

    public interface InPlayService
    {
        Task<IEnumerable<InPlayItem>> GetInPlayAsync(InPlayParam param);
    }

    public class InPlayServiceImpl : InPlayService
    {
        private readonly IDynamicRepository dynamicRepository;

        public InPlayServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<InPlayItem>> GetInPlayAsync(InPlayParam param)
            => await dynamicRepository.FetchAsync<InPlayItem>(new GetInPlayCriteria(param));
    }
}
