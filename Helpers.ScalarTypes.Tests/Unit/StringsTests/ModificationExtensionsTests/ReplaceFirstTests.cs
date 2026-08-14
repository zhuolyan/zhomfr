using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ReplaceFirstTests
{
    [TestCase(true)]
    [TestCase(false)]
    public void Should_ReplaceOnlyFirstOccurrence_When_MultipleExist(bool caseSensitive)
    {
        Assert.That(StubValues.VALUE.ReplaceFirst("the", "a", caseSensitive), Is.EqualTo("a quick brown fox jumps over the lazy dog"));
    }

    [Test]
    public void Should_ReturnOriginalValue_When_SearchNotFound()
    {
        Assert.That(StubValues.VALUE.ReplaceFirst(".", ","), Is.EqualTo(StubValues.VALUE));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.ReplaceFirst("x", "y"), Is.EqualTo(string.Empty));
    }

    [Test]
    public void Should_ReturnOriginalValue_When_SearchIsEmptyString()
    {
        Assert.That(StubValues.VALUE.ReplaceFirst(string.Empty, "y"), Is.EqualTo(StubValues.VALUE));
    }
}
