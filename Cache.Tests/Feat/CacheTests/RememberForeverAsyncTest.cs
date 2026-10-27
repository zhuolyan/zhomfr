using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Feat.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class RememberForeverAsyncTest(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_SetAndReturnValueWithRunFunction_When_NotExistWithoutTtl(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            ICache cache = GlobalSetup.GetTested(type);

            Assert.That((await cache.RememberForeverAsync<StubCached>(key, (_, _) => Task.FromResult(StubValues.NotExists), ct)).Value,
                        Is.EqualTo(StubValues.NotExists.Value));

            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValueWithoutRunClosure_When_ExistWithoutTtl(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            ICache cache = GlobalSetup.GetTested(type);

            Assert.That((await cache.RememberForeverAsync<StubCached>(key, (_, _) => Task.FromResult(StubValues.NotExists), ct)).Value,
                        Is.Not.EqualTo(StubValues.NotExists.Value));

            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
