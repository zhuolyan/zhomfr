using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Feat.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class PullTest(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_ReturnNull_When_NotExist(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);
        Assert.That(cache.Pull<StubCached>(key), Is.Null);
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnValueAndRemoveItem_When_NotExist(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Pull<StubCached>(key), Is.Not.Null);
            Assert.That(cache.Get<StubCached>(key), Is.Null);
        }
    }
}
