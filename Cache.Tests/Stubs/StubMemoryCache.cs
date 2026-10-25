using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Zhomfr.Cache.Tests.Stubs;

public class StubMemoryCache : MemoryCache
{
    /// <inheritdoc/>
    public StubMemoryCache(IOptions<MemoryCacheOptions> optionsAccessor) : base(optionsAccessor)
    {
    }

    /// <inheritdoc/>
    public StubMemoryCache(IOptions<MemoryCacheOptions> optionsAccessor, ILoggerFactory loggerFactory) : base(optionsAccessor, loggerFactory)
    {
    }
}
