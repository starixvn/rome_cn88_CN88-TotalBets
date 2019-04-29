namespace CN88.TotalBets.Features.ViewByBetTypes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
    using CN88.TotalBets.Features.ViewByBetTypes.Criteria;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;
    using CN88.TotalBets.Features.ViewByBetTypes.Params;
    using CN88.TotalBets.Shared.Constants;
    using Fanex.Data.Repository;

    public interface ViewByBetTypeService
    {
        Task<IDictionary<int, TotalBetsByBetType>> GetBetTypes(ViewByBetTypeParam param);
    }

    public class ViewByBetTypeServiceImpl : ViewByBetTypeService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly BetOptionBuilderFactory betOptionBuilderFactory;

        public ViewByBetTypeServiceImpl(
            IDynamicRepository dynamicRepository,
            BetOptionBuilderFactory betOptionBuilderFactory)
        {
            this.dynamicRepository = dynamicRepository;
            this.betOptionBuilderFactory = betOptionBuilderFactory;
        }

        public async Task<IDictionary<int, TotalBetsByBetType>> GetBetTypes(ViewByBetTypeParam param)
        {
            var betOptions = await dynamicRepository.FetchAsync<BetOption>(
                new GetTotalBetsByBetTypeCriteria(
                param.CustId,
                param.UserLevel,
                param.MatchId,
                param.StakeCondition,
                param.MarketType,
                param.PossType,
                param.Stake,
                param.EventType));

            var betOptionGroups = CreateBetOptionsGroups(param, betOptions);

            var otherBetTypeBetOptions = betOptions
                .Where(i => string.IsNullOrEmpty(i.BetTeam))
                .OrderBy(bo => bo.BetTypeId);

            AddOtherBetTypeOptionsBetOptionsToGroup(betOptionGroups, otherBetTypeBetOptions);

            return betOptionGroups;
        }

        private Dictionary<int, TotalBetsByBetType> CreateBetOptionsGroups(
            ViewByBetTypeParam param,
            IEnumerable<BetOption> betOptions)
        {
            var unionBetOptions = CreateDefaultBetOptions(betOptions, param.MarketType)
                .Select(bo =>
                {
                    var dbBetOption = betOptions.FirstOrDefault(dbbo => dbbo.Equals(bo));
                    if (dbBetOption != null)
                    {
                        bo.Stake = dbBetOption.Stake;
                        bo.Wager = dbBetOption.Wager;
                    }

                    return bo;
                });

            var betOptionGroups = unionBetOptions
                .GroupBy(option => option.BetTypeId)
                .OrderBy(group => group.Key)
                .ThenBy(group => betOptionBuilderFactory.IsUseDefaultBetOptionBuider(group.Key) ? 1 : 0)
                .ToDictionary(
                group => group.Key,
                group => BuildTotalBetsByBetType(group));

            return betOptionGroups;
        }

        private static void AddOtherBetTypeOptionsBetOptionsToGroup(Dictionary<int, TotalBetsByBetType> betOptionGroups, IOrderedEnumerable<BetOption> otherBetTypeBetOptions)
        {
            if (otherBetTypeBetOptions.Any())
            {
                betOptionGroups.Add(
                    (int)BetTypes.Others,
                    new TotalBetsByBetType(
                        (int)BetTypes.Others,
                        otherBetTypeBetOptions.Sum(bo => bo.Wager),
                        otherBetTypeBetOptions.Sum(bo => bo.Stake),
                        new List<BetOptionGroup> {
                            new BetOptionGroup(otherBetTypeBetOptions
                            .OrderBy(bo => bo.BetTypeId)
                            .ThenBy(bo => bo.IsLive ? 0 : 1),
                            false)
                        }));
            }
        }

        private IEnumerable<BetOption> CreateDefaultBetOptions(IEnumerable<BetOption> availableBetOptions, byte marketType)
        {
            var betOptions = new List<BetOption>();

            AddBetOptionHomeAway(availableBetOptions, betOptions, false);

            AddBetOptionOverUnder(availableBetOptions, betOptions, false);

            AddBetOptionOddEven(availableBetOptions, betOptions, false);

            AddBetOption1x2(availableBetOptions, betOptions, false);

            AddBetOptionHomeAway(availableBetOptions, betOptions, true);

            AddBetOptionOverUnder(availableBetOptions, betOptions, true);

            AddBetOptionOddEven(availableBetOptions, betOptions, true);

            AddBetOption1x2(availableBetOptions, betOptions, true);

            return betOptions
                .Where(betOption =>
                    marketType == MarketConstants.AllMarkets
                    || (betOption.IsLive && marketType == MarketConstants.InPlay)
                    || (!betOption.IsLive && marketType == MarketConstants.PreMatch));
        }

        private void AddBetOption1x2(IEnumerable<BetOption> availableBetOptions, List<BetOption> betOptions, bool isLive)
        {
            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes.FT_1X2 && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefault1x2BetOptions((int)BetTypes.FT_1X2, isLive));
            }

            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes.FirstHalf_1X2 && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefault1x2BetOptions((int)BetTypes.FirstHalf_1X2, isLive));
            }

            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes._2nd1x2 && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefault1x2BetOptions((int)BetTypes._2nd1x2, isLive));
            }
        }

        private void AddBetOptionOddEven(IEnumerable<BetOption> availableBetOptions, List<BetOption> betOptions, bool isLive)
        {
            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes._2ndOddEven && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultOddEvenBetOptions((int)BetTypes._2ndOddEven, isLive));
            }
        }

        private void AddBetOptionOverUnder(IEnumerable<BetOption> availableBetOptions, List<BetOption> betOptions, bool isLive)
        {
            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes._2nd_Over_Under && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultOverUnderBetOptions((int)BetTypes._2nd_Over_Under, isLive));
            }
        }

        private void AddBetOptionHomeAway(IEnumerable<BetOption> availableBetOptions, List<BetOption> betOptions, bool isLive)
        {
            AddBetOptionHomeAwayHandicap(availableBetOptions, betOptions, isLive);

            AddBetOptionHomeAwayMoneyLine(availableBetOptions, betOptions, isLive);

            AddBetOptionHomeAwayOverUnder(availableBetOptions, betOptions, isLive);

            AddBetOptionHomeAwayOddEven(availableBetOptions, betOptions, isLive);
        }

        private void AddBetOptionHomeAwayOddEven(IEnumerable<BetOption> availableBetOptions, List<BetOption> betOptions, bool isLive)
        {
            // For oddeven, because old business return betteam 'h', 'a'
            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes.Odd_Even && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultHomeAwayBetOptions((int)BetTypes.Odd_Even, isLive));
            }

            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes._1st_Odd_Even && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultHomeAwayBetOptions((int)BetTypes._1st_Odd_Even, isLive));
            }
        }

        private void AddBetOptionHomeAwayOverUnder(IEnumerable<BetOption> availableBetOptions, List<BetOption> betOptions, bool isLive)
        {
            // For overunder, because old business return betteam 'h', 'a'
            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes.Over_Under && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultHomeAwayBetOptions((int)BetTypes.Over_Under, isLive));
            }

            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes._1st_Over_Under && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultHomeAwayBetOptions((int)BetTypes._1st_Over_Under, isLive));
            }
        }

        private void AddBetOptionHomeAwayMoneyLine(IEnumerable<BetOption> availableBetOptions, List<BetOption> betOptions, bool isLive)
        {
            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes.Money_Line && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultHomeAwayBetOptions((int)BetTypes.Money_Line, isLive));
            }

            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes._1st_Money_Line && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultHomeAwayBetOptions((int)BetTypes._1st_Money_Line, isLive));
            }
        }

        private void AddBetOptionHomeAwayHandicap(IEnumerable<BetOption> availableBetOptions, List<BetOption> betOptions, bool isLive)
        {
            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes.Handicap && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultHomeAwayBetOptions((int)BetTypes.Handicap, isLive));
            }

            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes._1st_Handicap && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultHomeAwayBetOptions((int)BetTypes._1st_Handicap, isLive));
            }

            if (availableBetOptions.Any(bo => bo.BetTypeId == (int)BetTypes._2nd_HDP && bo.IsLive == isLive))
            {
                betOptions.AddRange(CreateDefaultHomeAwayBetOptions((int)BetTypes._2nd_HDP, isLive));
            }
        }

        private IEnumerable<BetOption> CreateDefaultHomeAwayBetOptions(int betTypeId, bool isLive)
            => new List<BetOption>
            {
                new BetOption(betTypeId, "h", isLive),
                new BetOption(betTypeId, "a", isLive)
            };

        private IEnumerable<BetOption> CreateDefaultOverUnderBetOptions(int betTypeId, bool isLive)
            => new List<BetOption>
            {
                new BetOption(betTypeId, "o", isLive),
                new BetOption(betTypeId, "u", isLive)
            };

        private IEnumerable<BetOption> CreateDefaultOddEvenBetOptions(int betTypeId, bool isLive)
            => new List<BetOption>
            {
                new BetOption(betTypeId, "o", isLive),
                new BetOption(betTypeId, "e", isLive)
            };

        private IEnumerable<BetOption> CreateDefault1x2BetOptions(int betTypeId, bool isLive)
            => new List<BetOption>
            {
                new BetOption(betTypeId, "1", isLive),
                new BetOption(betTypeId, "x", isLive),
                new BetOption(betTypeId, "2", isLive)
            };

        private static TotalBetsByBetType BuildTotalBetsByBetType(IGrouping<int, BetOption> group)
        {
            var totalWager = 0;
            var totalStake = 0m;
            var liveBetOptions = new List<BetOption>();
            var nonLiveBetOption = new List<BetOption>();

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
                     new BetOptionGroup(nonLiveBetOption, false),
                     new BetOptionGroup(liveBetOptions, true),
                });
        }
    }
}