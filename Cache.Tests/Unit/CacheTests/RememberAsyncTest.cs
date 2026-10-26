using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class RememberAsyncTest(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_SetAndReturnValueWithRunFunction_When_NotExistWithoutTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That((await cache.RememberAsync<StubCached>(key, null, (_, _) => Task.FromResult(StubValues.NotExists), ct)).Value,
                        Is.EqualTo(StubValues.NotExists.Value));

            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_SetAndReturnValueWithRunFunction_When_NotExistWithTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That((await cache.RememberAsync<StubCached>(key, new TimeSpan(0, 0, 1), (_, _) => Task.FromResult(StubValues.NotExists), ct)).Value,
                        Is.EqualTo(StubValues.NotExists.Value));

            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValueWithoutRunClosure_When_ExistWithoutTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That((await cache.RememberAsync<StubCached>(key, null, (_, _) => Task.FromResult(StubValues.NotExists), ct)).Value,
                        Is.Not.EqualTo(StubValues.NotExists.Value));

            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValueWithoutRunClosure_When_ExistWithTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That((await cache.RememberAsync<StubCached>(key, new TimeSpan(0, 0, 1), (_, _) => Task.FromResult(StubValues.NotExists), ct)).Value,
                        Is.Not.EqualTo(StubValues.NotExists.Value));

            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
