namespace CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders
{
    using System.Collections.Generic;

    public interface BetOptionBuilderFactory
    {
        IBetOptionBuilder CreateBetOptionBuilder(int betType);
    }

    public class BetOptionBuilderFactoryImpl : BetOptionBuilderFactory
    {
        private static readonly IBetOptionBuilder DefaultBetOptionBuilder = new BetOptionBuilder1();

        private static readonly IDictionary<int, IBetOptionBuilder> BetOptionBuilderContainer
            = new Dictionary<int, IBetOptionBuilder>
            {
                { 2, new BetOptionBuilder2() },
                { 12, new BetOptionBuilder2() },
                { 428, new BetOptionBuilder428() },
                { 3, new BetOptionBuilder3() },
                { 8, new BetOptionBuilder3() },
                { 18, new BetOptionBuilder18() },
                { 5, new BetOptionBuilder5() },
                { 15, new BetOptionBuilder5() },
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
    }
}