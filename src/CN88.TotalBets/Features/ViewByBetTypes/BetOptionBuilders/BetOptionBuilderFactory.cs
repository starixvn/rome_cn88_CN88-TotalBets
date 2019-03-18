namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;

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
                { 1, new BetOptionBuilder1() },
                { 7, new BetOptionBuilder1() },
                { 17, new BetOptionBuilder1() },
                { 20, new BetOptionBuilder1() },
                { 21, new BetOptionBuilder1() },

                { 2, new BetOptionBuilder2() },
                { 12, new BetOptionBuilder2() },
                { 184, new BetOptionBuilder428() },
                { 428, new BetOptionBuilder428() },

                { 3, new BetOptionBuilder3() },
                { 8, new BetOptionBuilder3() },
                { 18, new BetOptionBuilder18() },

                { 5, new BetOptionBuilder5() },
                { 15, new BetOptionBuilder5() },
                { 177, new BetOptionBuilder5() },
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