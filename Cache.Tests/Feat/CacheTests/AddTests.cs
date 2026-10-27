using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Feat.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class AddTests(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_ReturnTrue_When_AddingNotExistsOrExpiredWithoutTtl(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Add(key, StubValues.NotExists), Is.True);
            Assert.That(cache.Get<StubCached>(key)?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_ReturnTrue_When_AddingNotExistsOrExpiredWithTtl(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Add(key, StubValues.NotExists, new TimeSpan(0, 0, 1)), Is.True);
            Assert.That(cache.Get<StubCached>(key)?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnFalse_When_AddingExistsWithoutTtl(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Add(key, StubValues.NotExists), Is.False);
            Assert.That(cache.Get<StubCached>(key)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnFalse_When_AddingExistsWithTtl(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Add(key, StubValues.NotExists, new TimeSpan(0, 0, 1)), Is.False);
            Assert.That(cache.Get<StubCached>(key)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
