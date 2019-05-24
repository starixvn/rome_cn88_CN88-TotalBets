namespace CN88.TotalBets.Tests.Features.ViewByBetTypes
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CN88.TotalBets.Features.ViewByBetTypes;
    using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
    using CN88.TotalBets.Features.ViewByBetTypes.Models;
    using CN88.TotalBets.Shared.Constants;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;
    using CN88.TotalBets.Features.ViewByBetTypes.Criteria;
    using CN88.TotalBets.Features.ViewByBetTypes.Params;
    using CN88.TotalBets.Shared.Models;

    [Trait("Category", "ViewByBetTypes")]
    public class ViewByBetTypeServiceTests
    {
        private readonly ViewByBetTypeServiceImpl viewByBetTypeService;
        private readonly IDynamicRepository dynamicRepository;
        private readonly BetOptionBuilderFactory betOptionBuilderFactory;

        public ViewByBetTypeServiceTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            betOptionBuilderFactory = Substitute.For<BetOptionBuilderFactory>();
            viewByBetTypeService = new ViewByBetTypeServiceImpl(dynamicRepository, betOptionBuilderFactory);
        }

        [Fact]
        public async Task GetTotalBetsByBetTypes_BetTypeIsHandicap_ReturnHandicapTotalBets()
        {
            var stubBetOptions = StubBetOptions().Where(bo =>
                bo.BetTypeId == (int)BetTypes._2nd_HDP
                || bo.BetTypeId == (int)BetTypes.Handicap
                || bo.BetTypeId == (int)BetTypes._1st_Handicap);
            var param = CreateViewByBetTypeParam(MarketConstants.AllMarkets, EventTypeConstants.InPlay);

            dynamicRepository
                .FetchAsync<BetOption>(Arg.Is<GetTotalBetsByBetTypeCriteria>(
                    criteria => criteria.CustId == param.CustId && criteria.MatchId == param.MatchId))
                .Returns(stubBetOptions);

            var totalBets = await viewByBetTypeService.GetTotalBetsByBetTypes(param);

            var handicapItem1 = totalBets[(int)BetTypes.Handicap].BetOptionGroups.ElementAt(0);
            var handicapItem2 = totalBets[(int)BetTypes.Handicap].BetOptionGroups.ElementAt(1);

            // Grop By BetType
            Assert.Equal(3, totalBets.Keys.Count);
            Assert.Equal((int)BetTypes.Handicap, totalBets.Keys.ElementAt(0));
            Assert.Equal((int)BetTypes._1st_Handicap, totalBets.Keys.ElementAt(1));
            Assert.Equal((int)BetTypes._2nd_HDP, totalBets.Keys.ElementAt(2));

            // Group by InPlay/PreMatch, betoptions, default betoption
            Assert.Equal(2, totalBets[(int)BetTypes.Handicap].TotalWager);
            Assert.Equal(3, totalBets[(int)BetTypes.Handicap].TotalStake);
            Assert.Equal(2, totalBets[(int)BetTypes.Handicap].BetOptionGroups.Count());
            Assert.False(handicapItem1.IsLive);
            Assert.Equal("Pre-Match", handicapItem1.LiveText);
            Assert.Equal(2, handicapItem1.BetOptions.First().Stake);
            Assert.Equal(0, handicapItem1.BetOptions.ElementAt(1).Stake);
            Assert.Equal("In-Play", handicapItem2.LiveText);
            Assert.Equal(0, handicapItem2.BetOptions.First().Stake);
            Assert.Equal(1, handicapItem2.BetOptions.ElementAt(1).Stake);

            Assert.Equal(2, totalBets[(int)BetTypes._2nd_HDP].TotalWager);
            Assert.Equal(4, totalBets[(int)BetTypes._2nd_HDP].TotalStake);

            Assert.Equal(2, totalBets[(int)BetTypes._1st_Handicap].TotalWager);
            Assert.Equal(5, totalBets[(int)BetTypes._1st_Handicap].TotalStake);
        }

        [Fact]
        public async Task GetTotalBetsByBetTypes_BetTypeIsHandicapAndPreMatchMarket_ReturnHandicapTotalBets()
        {
            var stubBetOptions = StubBetOptions().Where(bo =>
                bo.BetTypeId == (int)BetTypes._2nd_HDP
                || bo.BetTypeId == (int)BetTypes.Handicap
                || bo.BetTypeId == (int)BetTypes._1st_Handicap);
            var param = CreateViewByBetTypeParam(MarketConstants.PreMatch, EventTypeConstants.InPlay);

            dynamicRepository
                .FetchAsync<BetOption>(Arg.Is<GetTotalBetsByBetTypeCriteria>(
                    criteria => criteria.CustId == param.CustId && criteria.MatchId == param.MatchId))
                .Returns(stubBetOptions);

            var totalBets = await viewByBetTypeService.GetTotalBetsByBetTypes(param);

            var handicapItem1 = totalBets[(int)BetTypes.Handicap].BetOptionGroups.ElementAt(0);

            // Grop By BetType
            Assert.Equal(3, totalBets.Keys.Count);
            Assert.Equal((int)BetTypes.Handicap, totalBets.Keys.ElementAt(0));
            Assert.Equal((int)BetTypes._1st_Handicap, totalBets.Keys.ElementAt(1));
            Assert.Equal((int)BetTypes._2nd_HDP, totalBets.Keys.ElementAt(2));

            // Group by InPlay/PreMatch, betoptions, default betoption
            Assert.Equal(1, totalBets[(int)BetTypes.Handicap].TotalWager);
            Assert.Equal(2, totalBets[(int)BetTypes.Handicap].TotalStake);
            Assert.Equal(2, totalBets[(int)BetTypes.Handicap].BetOptionGroups.Count());
            Assert.False(handicapItem1.IsLive);
            Assert.Equal("Pre-Match", handicapItem1.LiveText);
            Assert.Equal(2, handicapItem1.BetOptions.First().Stake);
            Assert.Equal(0, handicapItem1.BetOptions.ElementAt(1).Stake);

            Assert.Equal(1, totalBets[(int)BetTypes._2nd_HDP].TotalWager);
            Assert.Equal(1, totalBets[(int)BetTypes._2nd_HDP].TotalStake);

            Assert.Equal(1, totalBets[(int)BetTypes._1st_Handicap].TotalWager);
            Assert.Equal(1, totalBets[(int)BetTypes._1st_Handicap].TotalStake);
        }

        [Fact]
        public async Task GetTotalBetsByBetTypes_BetTypeIsOverUnder_ReturnOverUnderTotalBets()
        {
            var stubBetOptions = StubBetOptions().Where(bo =>
                bo.BetTypeId == (int)BetTypes.Over_Under
                || bo.BetTypeId == (int)BetTypes._1st_Over_Under
                || bo.BetTypeId == (int)BetTypes._2nd_Over_Under);
            var param = CreateViewByBetTypeParam(MarketConstants.AllMarkets, EventTypeConstants.InPlay);

            dynamicRepository
                .FetchAsync<BetOption>(Arg.Is<GetTotalBetsByBetTypeCriteria>(
                    criteria => criteria.CustId == param.CustId && criteria.MatchId == param.MatchId))
                .Returns(stubBetOptions);

            var totalBets = await viewByBetTypeService.GetTotalBetsByBetTypes(param);

            var overUnderItem1 = totalBets[(int)BetTypes.Over_Under].BetOptionGroups.ElementAt(0);
            var overUnderItem2 = totalBets[(int)BetTypes.Over_Under].BetOptionGroups.ElementAt(1);

            // Grop By BetType
            Assert.Equal(3, totalBets.Keys.Count);
            Assert.Equal((int)BetTypes.Over_Under, totalBets.Keys.ElementAt(0));
            Assert.Equal((int)BetTypes._1st_Over_Under, totalBets.Keys.ElementAt(1));
            Assert.Equal((int)BetTypes._2nd_Over_Under, totalBets.Keys.ElementAt(2));

            // Group by InPlay/PreMatch, betoptions, default betoption
            Assert.Equal(2, totalBets[(int)BetTypes.Over_Under].TotalWager);
            Assert.Equal(3, totalBets[(int)BetTypes.Over_Under].TotalStake);
            Assert.Equal(2, totalBets[(int)BetTypes.Over_Under].BetOptionGroups.Count());
            Assert.False(overUnderItem1.IsLive);
            Assert.Equal("Pre-Match", overUnderItem1.LiveText);
            Assert.Equal(1, overUnderItem1.BetOptions.ElementAt(0).Stake);
            Assert.Equal(0, overUnderItem1.BetOptions.ElementAt(1).Stake);
            Assert.Equal("In-Play", overUnderItem2.LiveText);
            Assert.Equal(2, overUnderItem2.BetOptions.ElementAt(0).Stake);
            Assert.Equal(0, overUnderItem2.BetOptions.ElementAt(1).Stake);

            Assert.Equal(2, totalBets[(int)BetTypes._1st_Over_Under].TotalWager);
            Assert.Equal(8, totalBets[(int)BetTypes._1st_Over_Under].TotalStake);

            Assert.Equal(2, totalBets[(int)BetTypes._2nd_Over_Under].TotalWager);
            Assert.Equal(10, totalBets[(int)BetTypes._2nd_Over_Under].TotalStake);
        }

        private ViewByBetTypeParam CreateViewByBetTypeParam(byte marketType, byte eventType)
            => new ViewByBetTypeParam(1, UserLevel.Super, 123, 1, 100, 1, marketType, eventType);

        private IEnumerable<BetOption> StubBetOptions()
            => new List<BetOption>
            {
                new BetOption((int)BetTypes._2nd_HDP, "a", false, 1, 1),
                new BetOption((int)BetTypes.Handicap, "h", false, 1, 2),
                new BetOption((int)BetTypes._1st_Handicap, "a", false, 1, 1),
                new BetOption((int)BetTypes._2nd_HDP, "a", true, 1, 3),
                new BetOption((int)BetTypes.Handicap, "a", true, 1, 1),
                new BetOption((int)BetTypes._1st_Handicap, "a", true, 1, 4),

                new BetOption((int)BetTypes.Over_Under, "h", false, 1, 1),
                new BetOption((int)BetTypes._1st_Over_Under, "h", false, 1, 3),
                new BetOption((int)BetTypes._2nd_Over_Under, "o", false, 1, 4),
                new BetOption((int)BetTypes.Over_Under, "h", true, 1, 2),
                new BetOption((int)BetTypes._1st_Over_Under, "h", true, 1, 5),
                new BetOption((int)BetTypes._2nd_Over_Under, "o", true, 1, 6),

                new BetOption((int)BetTypes.FT_1X2, "1", false, 1, 1),
                new BetOption((int)BetTypes.FirstHalf_1X2, "x", false, 1, 1),
                new BetOption((int)BetTypes._2nd1x2, "2", false, 1, 1),
                new BetOption((int)BetTypes.FT_1X2, "1", true, 1, 1),
                new BetOption((int)BetTypes.FirstHalf_1X2, "x", true, 1, 1),
                new BetOption((int)BetTypes._2nd1x2, "2", true, 1, 1),

                new BetOption((int)BetTypes.Odd_Even, "h", false, 1, 1),
                new BetOption((int)BetTypes.Odd_Even, "a", false, 1, 2),
                new BetOption((int)BetTypes._1st_Odd_Even, "h", false, 1, 2),
                new BetOption((int)BetTypes._2ndOddEven, "e", false, 1, 2),
                new BetOption(428, "o", false, 1, 1),
                new BetOption((int)BetTypes.Odd_Even, "h", true, 1, 2),
                new BetOption((int)BetTypes.Odd_Even, "a", true, 1, 1),
                new BetOption((int)BetTypes._1st_Odd_Even, "h", true, 1, 2),
                new BetOption((int)BetTypes._2ndOddEven, "o", true, 1, 2),
                new BetOption(428, "e", true, 1, 1),

                new BetOption((int)BetTypes.Money_Line, "h", false, 1, 1),
                new BetOption((int)BetTypes._1st_Money_Line, "a", false, 1, 1),
                new BetOption((int)BetTypes.Money_Line, "h", true, 1, 1),
                new BetOption((int)BetTypes._1st_Money_Line, "a", true, 1, 1),

                new BetOption((int)BetTypes.FirstCorrectScore, string.Empty, false, 1, 1),
                new BetOption((int)BetTypes.Home_No_Bet, string.Empty, false, 1, 1),
                new BetOption((int)BetTypes.FirstCorrectScore, string.Empty, true, 1, 1),
                new BetOption((int)BetTypes.Home_No_Bet, string.Empty, true, 1, 1),
            };
    }
}
