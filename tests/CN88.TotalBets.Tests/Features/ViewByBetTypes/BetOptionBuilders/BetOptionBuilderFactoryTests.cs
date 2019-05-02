namespace CN88.TotalBets.Tests.Features.ViewByBetTypes.BetOptionBuilders
{
    using CN88.TotalBets.Features.ViewByBetTypes.BetOptionBuilders;
    using Xunit;

    [Trait("Category", "ViewByBetTypes")]
    public class BetOptionBuilderFactoryTests
    {
        private readonly BetOptionBuilderFactoryImpl betOptionBuilderFactory;

        public BetOptionBuilderFactoryTests()
        {
            betOptionBuilderFactory = new BetOptionBuilderFactoryImpl();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(17)]
        [InlineData(20)]
        [InlineData(21)]
        public void CreateBetOptionBuilder_BetTypeUsesBuilder1_ReturnBetOptionBuilder1Instance(int betType)
        {
            var builder = betOptionBuilderFactory.CreateBetOptionBuilder(betType);

            Assert.IsType<BetOptionBuilder1>(builder);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(12)]
        public void CreateBetOptionBuilder_BetTypeUsesBuilder2_ReturnBetOptionBuilder2Instance(int betType)
        {
            var builder = betOptionBuilderFactory.CreateBetOptionBuilder(betType);

            Assert.IsType<BetOptionBuilder2>(builder);
        }

        [Theory]
        [InlineData(184)]
        [InlineData(428)]
        public void CreateBetOptionBuilder_BetTypeUsesBuilder428_ReturnBetOptionBuilder428Instance(int betType)
        {
            var builder = betOptionBuilderFactory.CreateBetOptionBuilder(betType);

            Assert.IsType<BetOptionBuilder428>(builder);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(8)]
        public void CreateBetOptionBuilder_BetTypeUsesBuilder3_ReturnBetOptionBuilder3Instance(int betType)
        {
            var builder = betOptionBuilderFactory.CreateBetOptionBuilder(betType);

            Assert.IsType<BetOptionBuilder3>(builder);
        }

        [Theory]
        [InlineData(18)]
        public void CreateBetOptionBuilder_BetTypeUsesBuilder18_ReturnBetOptionBuilder18Instance(int betType)
        {
            var builder = betOptionBuilderFactory.CreateBetOptionBuilder(betType);

            Assert.IsType<BetOptionBuilder18>(builder);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(15)]
        [InlineData(177)]
        [InlineData(430)]
        public void CreateBetOptionBuilder_BetTypeUsesBuilder5_ReturnBetOptionBuilder5Instance(int betType)
        {
            var builder = betOptionBuilderFactory.CreateBetOptionBuilder(betType);

            Assert.IsType<BetOptionBuilder5>(builder);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(32)]
        [InlineData(111)]
        [InlineData(222)]
        public void CreateBetOptionBuilder_BetTypeUsesDefaultBuilder_ReturnDefaultBetOptionBuilderInstance(int betType)
        {
            var builder = betOptionBuilderFactory.CreateBetOptionBuilder(betType);

            Assert.IsType<DefaultBetOptionBuilder>(builder);
        }
    }
}