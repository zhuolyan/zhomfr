using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ReplaceTests
{
    [TestCase(true)]
    [TestCase(false)]
    public void Should_ReplaceOnlyFirstOccurrence_When_MultipleExist(bool caseSensitive)
    {
        Assert.That(StubValues.VALUE.Replace("the", ["an", "a"], caseSensitive), Is.EqualTo("an quick brown fox jumps over a lazy dog"));
    }

    [Test]
    public void Should_ReturnOriginalValue_When_SearchNotFound()
    {
        Assert.That(StubValues.VALUE.Replace(".", ["an", "a"]), Is.EqualTo(StubValues.VALUE));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.Replace("x", ["y", "z"]), Is.EqualTo(string.Empty));
    }

    [Test]
    public void Should_ReturnOriginalValue_When_SearchIsEmptyString()
    {
        Assert.That(StubValues.VALUE.Replace(string.Empty, ["y", "z"]), Is.EqualTo(StubValues.VALUE));
    }
}
