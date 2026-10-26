using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class ForgetAsyncTests(TestTypes type)
{
    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_Exists(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.ForgetAsync(key, ct)!, Is.True);
            Assert.That(await cache.GetAsync<StubCached>(key, ct), Is.Null);
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_NotExist(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.ForgetAsync(key, ct)!, Is.False);
            Assert.That(await cache.GetAsync<StubCached>(key, ct), Is.Null);
        }
    }
}
