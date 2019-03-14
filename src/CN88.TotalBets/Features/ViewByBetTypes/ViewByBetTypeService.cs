namespace CN88.TotalBets.Features.ViewByBetTypes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CN88.TotalBets.Features.ViewByBetTypes.Criteria;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;
    using CN88.TotalBets.Features.ViewByBetTypes.Params;
    using Fanex.Data.Repository;

    public interface ViewByBetTypeService
    {
        Task<IDictionary<int, TotalBetsByBetType>> GetBetTypes(ViewByBetTypeParam param);
    }

    public class ViewByBetTypeServiceImpl : ViewByBetTypeService
    {
        private readonly IDynamicRepository dynamicRepository;

        public ViewByBetTypeServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IDictionary<int, TotalBetsByBetType>> GetBetTypes(ViewByBetTypeParam param)
        {
            var betOptions = await dynamicRepository.FetchAsync<BaseBetOption>(
                new GetTotalBetsByBetTypeCriteria(
                param.CustId,
                param.UserLevel,
                param.MatchId,
                param.StakeCondition,
                param.MarketType,
                param.PossType,
                param.Stake));

            var betOptionGroups = betOptions
                .OrderBy(option => option.BetTypeId)
                .GroupBy(option => option.BetTypeId)
                .ToDictionary(
                group => group.Key,
                group => BuildTotalBetsByBetType(group));

            return betOptionGroups;
        }

        private static TotalBetsByBetType BuildTotalBetsByBetType(IGrouping<int, BaseBetOption> group)
        {
            var totalWager = 0;
            var totalStake = 0m;
            var liveBetOptions = new List<BaseBetOption>();
            var nonLiveBetOption = new List<BaseBetOption>();

            foreach (var item in group)
            {
                totalWager += item.Wager;
                totalStake += item.Stake;
                if (item.IsLive)
                {
                    liveBetOptions.Add(item);
                }
                else
                {
                    nonLiveBetOption.Add(item);
                }
            }

            return new TotalBetsByBetType(
                group.Key,
                totalWager,
                totalStake,
                new List<BetOptionGroup>
                {
                     new BetOptionGroup(nonLiveBetOption.OrderByDescending(option => option.Stake), false),
                     new BetOptionGroup(liveBetOptions.OrderByDescending(option => option.Stake), true),
                });
        }
    }
}