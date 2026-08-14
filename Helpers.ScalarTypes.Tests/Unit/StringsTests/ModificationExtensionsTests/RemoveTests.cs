using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class RemoveTests
{
    [Test]
    public void Should_RemoveMultipleNeedles_When_Called()
    {
        Assert.That(StubValues.VALUE.Remove("quick", "brown", "fox", ""), Is.EqualTo("the    jumps over the lazy dog"));
    }

    [Test]
    public void Should_ReturnOriginalValue_When_NoNeedlesProvided()
    {
        const string VALUE = "test";

        Assert.That(VALUE.Remove(), Is.EqualTo(VALUE));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.Remove("hello "), Is.EqualTo(string.Empty));
    }
}
