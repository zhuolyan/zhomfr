using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Feat.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class ForgetTests(TestTypes type)
{
    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnTrue_When_Exists(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Forget(key), Is.True);
            Assert.That(cache.Get<StubCached>(key), Is.Null);
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_ReturnFalse_When_NotExist(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Forget(key), Is.False);
            Assert.That(cache.Get<StubCached>(key), Is.Null);
        }
    }
}
