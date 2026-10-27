using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Feat.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class RememberForeverTest(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_SetAndReturnValueWithRunFunction_When_NotExistWithoutTtl(string key)
    {
        using (Assert.EnterMultipleScope()) {
            ICache cache = GlobalSetup.GetTested(type);

            Assert.That(cache.RememberForever<StubCached>(key, _ => StubValues.NotExists).Value, Is.EqualTo(StubValues.NotExists.Value));

            Assert.That(cache.Get<StubCached>(key)?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnValueWithoutRunClosure_When_ExistWithoutTtl(string key)
    {
        using (Assert.EnterMultipleScope()) {
            ICache cache = GlobalSetup.GetTested(type);

            Assert.That(cache.RememberForever<StubCached>(key, _ => StubValues.NotExists).Value, Is.Not.EqualTo(StubValues.NotExists.Value));

            Assert.That(cache.Get<StubCached>(key)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
