using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class HasTest(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_ReturnFalse_When_NotExist(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        Assert.That(cache.Has(key), Is.False);
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public void Should_ReturnTrue_When_Exists(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        Assert.That(cache.Has(key), Is.True);
    }
}
