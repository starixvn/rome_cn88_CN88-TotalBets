using System.Linq;
using AutoFixture;
using CN88.TotalBets.Shared.Models;

namespace CN88.TotalBets.Tests
{
    public static class Util
    {
        public static UserLevel RandomSMALevel(Fixture fixture)
        {
            var custLevelValue = fixture
                .Create<Generator<byte>>()
                .First(x => x >= UserLevel.Agent.Value && x <= UserLevel.Super.Value);

            return Enumeration.FromValue<UserLevel>(custLevelValue);
        }

        public static int RandomValidCustId(Fixture fixture)
        {
            return fixture
                .Create<Generator<int>>()
                .First(x => x > 0);
        }
    }
}