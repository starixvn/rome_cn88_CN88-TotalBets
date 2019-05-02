namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;
    using CN88.TotalBets.Shared.Constants;

    public interface BetOptionBuilderFactory
    {
        IBetOptionBuilder CreateBetOptionBuilder(int betType);

        bool IsUseDefaultBetOptionBuider(int betType);
    }

    public class BetOptionBuilderFactoryImpl : BetOptionBuilderFactory
    {
        private static readonly IBetOptionBuilder DefaultBetOptionBuilder = new DefaultBetOptionBuilder();

        private static readonly IDictionary<int, IBetOptionBuilder> BetOptionBuilderContainer
            = new Dictionary<int, IBetOptionBuilder>
            {
                { (int)BetTypes.Handicap, new BetOptionBuilder1() },
                { (int)BetTypes._1st_Handicap, new BetOptionBuilder1() },
                { (int)BetTypes._2nd_HDP, new BetOptionBuilder1() },
                { (int)BetTypes.Money_Line, new BetOptionBuilder1() },
                { (int)BetTypes._1st_Money_Line, new BetOptionBuilder1() },

                { (int)BetTypes.Odd_Even, new BetOptionBuilder2() },
                { (int)BetTypes._1st_Odd_Even, new BetOptionBuilder2() },
                { (int)BetTypes._2ndOddEven, new BetOptionBuilder428() },
                { 428, new BetOptionBuilder428() },

                { (int)BetTypes.Over_Under, new BetOptionBuilder3() },
                { (int)BetTypes._1st_Over_Under, new BetOptionBuilder3() },
                { (int)BetTypes._2nd_Over_Under, new BetOptionBuilder18() },

                { (int)BetTypes.FT_1X2, new BetOptionBuilder5() },
                { (int)BetTypes.FirstHalf_1X2, new BetOptionBuilder5() },
                { (int)BetTypes._2nd1x2, new BetOptionBuilder5() },
                { 430, new BetOptionBuilder5() }
            };

        public IBetOptionBuilder CreateBetOptionBuilder(int betType)
        {
            if (BetOptionBuilderContainer.ContainsKey(betType))
            {
                return BetOptionBuilderContainer[betType];
            }

            return DefaultBetOptionBuilder;
        }

        public bool IsUseDefaultBetOptionBuider(int betType)
            => !BetOptionBuilderContainer.ContainsKey(betType);
    }
}