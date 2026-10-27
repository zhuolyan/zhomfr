using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Feat.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class GetAsyncTests(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnNull_When_NotExist(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);
        Assert.That(await cache.GetAsync<StubCached>(key, ct), Is.Null);
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnDefaultValue_When_NotExistWithDefaultValue(string key, CancellationToken ct = default)
    {
        ICache cache = GlobalSetup.GetTested(type);
        Assert.That((await cache.GetAsync(key, StubValues.NotExists, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValue_When_AddingExists(string key, CancellationToken ct)
    {
        ICache      cache = GlobalSetup.GetTested(type);
        StubCached? test  = await cache.GetAsync<StubCached>(key, ct);
        Assert.That(test, Is.Not.Null);
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValue_When_ExistWithDefaultValue(string key, CancellationToken ct = default)
    {
        ICache cache = GlobalSetup.GetTested(type);
        Assert.That((await cache.GetAsync(key, StubValues.NotExists, ct))?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
    }
}
