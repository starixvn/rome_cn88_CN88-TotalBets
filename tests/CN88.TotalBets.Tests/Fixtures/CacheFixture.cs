using Fanex.Caching;
using NSubstitute;

namespace CN88.TotalBets.Tests.Fixtures
{
    public class CacheFixture
    {
        public CacheFixture()
        {
            CacheForTesting = Substitute.For<Cache>();
            ConfiguredCacheService.SwitchCacheStorage(CacheForTesting);
        }

        public Cache CacheForTesting { get; private set; }
    }
}