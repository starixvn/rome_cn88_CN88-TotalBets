using CN88.TotalBets.Tests.Fixtures;
using Xunit;

namespace CN88.TotalBets.Tests.Collections
{
    [CollectionDefinition("CacheCollection")]
    public class CacheCollection : ICollectionFixture<CacheFixture>
    {
    }
}