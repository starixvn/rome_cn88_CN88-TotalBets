namespace CN88.TotalBets.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Criteria;
    using Fanex.Data.Repository;
    using Models.InPlay;
    using Params;

    public interface InPlayService
    {
        Task<IEnumerable<InPlayItem>> GetInPlayAsync(GetInPlayParam param);
    }

    public class InPlayServiceImpl : InPlayService
    {
        private readonly IDynamicRepository dynamicRepository;

        public InPlayServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<InPlayItem>> GetInPlayAsync(GetInPlayParam param)
            => await dynamicRepository.FetchAsync<InPlayItem>(ToGetInPlayCriteria(param));

        private static CriteriaBase ToGetInPlayCriteria(GetInPlayParam param)
            => new GetInPlayCriteria(
                param.CustId,
                param.UserLevel,
                param.Type,
                param.SportTypeGroup,
                param.LeagueId,
                param.StakeCondition,
                param.Stake,
                param.PossType,
                param.IsLive);
    }
}
