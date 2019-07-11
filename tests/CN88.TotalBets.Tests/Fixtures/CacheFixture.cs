using Fanex.Caching;
using NSubstitute;

namespace CN88.TotalBets.Tests.Fixtures
{
    public class CacheFixture
    {
        public CacheFixture()
        {
            CacheForTesting = Substitute.For<Cache>();
#pragma warning disable CS0618 // Type or member is obsolete
            ConfiguredCacheService.SwitchCacheStorage(CacheForTesting);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public Cache CacheForTesting { get; private set; }
    }
}