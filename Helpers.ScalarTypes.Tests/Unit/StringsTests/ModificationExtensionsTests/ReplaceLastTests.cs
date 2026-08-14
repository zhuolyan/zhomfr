using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ReplaceLastTests
{
    [TestCase(true)]
    [TestCase(false)]
    public void Should_ReplaceOnlyLastOccurrence_When_MultipleExist(bool caseSensitive)
    {
        Assert.That(StubValues.VALUE.ReplaceLast("the", "a", caseSensitive), Is.EqualTo("the quick brown fox jumps over a lazy dog"));
    }

    [Test]
    public void Should_ReturnOriginalValue_When_SearchNotFound()
    {
        Assert.That(StubValues.VALUE.ReplaceLast(".", ","), Is.EqualTo(StubValues.VALUE));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.ReplaceLast("x", "y"), Is.EqualTo(string.Empty));
    }

    [Test]
    public void Should_ReturnOriginalValue_When_SearchIsEmptyString()
    {
        Assert.That(StubValues.VALUE.ReplaceLast(string.Empty, "y"), Is.EqualTo(StubValues.VALUE));
    }
}
