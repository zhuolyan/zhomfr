using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class RemoveIgnoreCaseTests
{
    [Test]
    public void Should_RemoveMultipleNeedles_When_Called()
    {
        Assert.That(StubValues.VALUE.RemoveIgnoreCase("Quick", "brOwn", "FOX", ""), Is.EqualTo("the    jumps over the lazy dog"));
    }

    [Test]
    public void Should_ReturnOriginalValue_When_NoNeedlesProvided()
    {
        const string VALUE = "test";

        Assert.That(VALUE.RemoveIgnoreCase(), Is.EqualTo(VALUE));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.RemoveIgnoreCase("hello "), Is.EqualTo(string.Empty));
    }
}
