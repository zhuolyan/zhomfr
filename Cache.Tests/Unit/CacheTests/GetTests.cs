using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class GetTests(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_ReturnNull_When_NotExist(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);
        Assert.That(cache.Get<StubCached>(key), Is.Null);
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_ReturnDefaultValue_When_NotExistWithDefaultValue(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);
        Assert.That(cache.Get(key, StubValues.NotExists)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnValue_When_AddingExists(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);
        Assert.That(cache.Get<StubCached>(key), Is.Not.Null);
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnValue_When_ExistWithDefaultValue(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);
        Assert.That(cache.Get(key, StubValues.NotExists)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
    }
}
