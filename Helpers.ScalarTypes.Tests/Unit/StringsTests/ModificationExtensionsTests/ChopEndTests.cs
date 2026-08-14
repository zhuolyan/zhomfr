using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ChopEndTests
{
    [Test]
    public void Should_RemoveSuffix_When_ItExistsAtEnd()
    {
        Assert.That(StubValues.CHOP_END_VALUE.ChopEnd(".php"), Is.EqualTo("file.test"));
    }

    [Test]
    public void Should_RemoveFirstMatchingPrefix_When_MultipleAreProvided()
    {
        Assert.That(StubValues.CHOP_END_VALUE.ChopEnd(".js", ".php"), Is.EqualTo("file.test"));
    }

    [Test]
    public void Should_RemoveOriginalValue_When_NothingProvided()
    {
        Assert.That(StubValues.CHOP_END_VALUE.ChopEnd(), Is.EqualTo(StubValues.CHOP_END_VALUE));
    }
}
