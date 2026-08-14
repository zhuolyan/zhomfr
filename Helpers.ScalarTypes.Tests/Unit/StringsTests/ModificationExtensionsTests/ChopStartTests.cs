using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ChopStartTests
{
    [Test]
    public void Should_RemovePrefix_When_ItExistsAtStart()
    {
        Assert.That(StubValues.CHOP_START_VALUE.ChopStart("https://"), Is.EqualTo("example.com"));
    }

    [Test]
    public void Should_RemoveFirstMatchingPrefix_When_MultipleAreProvided()
    {
        Assert.That(StubValues.CHOP_START_VALUE.ChopStart("https://", "http://"), Is.EqualTo("example.com"));
    }

    [TestCase("")]
    [TestCase("aaa")]
    public void Should_ReturnOriginalValue_When_SearchNotExistAtStart(string search)
    {
        Assert.That(StubValues.CHOP_START_VALUE.ChopStart(search), Is.EqualTo(StubValues.CHOP_START_VALUE));
    }
}
