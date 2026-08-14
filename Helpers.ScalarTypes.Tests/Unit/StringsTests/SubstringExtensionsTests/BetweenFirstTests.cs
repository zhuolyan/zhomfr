using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.SubstringExtensionsTests;

[TestFixture]
public class BetweenFirstTests
{
    [TestCase("the quick brown fox jumps over the lazy dog", "the ", "fox ", "quick brown ")]
    [TestCase("[a] bc [d]", "[", "]", "a")]
    [TestCase("[a] bc [d]", "[", "*", "a] bc [d]")]
    [TestCase("[a] bc [d]", "*", "]", "[a")]
    public void Should_ReturnSubstringBetweenFirstFindAfterAndLastFindBefore_When_SubstringExists(string input, string after, string before, string expected)
    {
        Assert.That(input.BetweenFirst(after, before), Is.EqualTo(expected));
    }

    [TestCase("the quick brown fox jumps over the lazy dog", "*", "+")]
    [TestCase("[a] bc [d]", "*", "+")]
    public void Should_ReturnOriginalValue_When_SubstringNotExists(string input, string after, string before)
    {
        Assert.That(input.BetweenFirst(after, before), Is.EqualTo(input));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.BetweenFirst("x", "y"), Is.EqualTo(string.Empty));
    }
}
