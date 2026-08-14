using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.SubstringExtensionsTests;

[TestFixture]
public class BetweenLastTests
{
    [TestCase("the quick brown fox jumps over the lazy dog", "the ", "fox ", "lazy dog")]
    [TestCase("[a] bc [d]", "[", "]", "d")]
    [TestCase("[a] bc [d]", "[", "*", "d]")]
    [TestCase("[a] bc [d]", "*", "]", "[a] bc [d")]
    public void Should_ReturnSubstringAfterFirstFind_When_SubstringExists(string input, string after, string before, string expected)
    {
        Assert.That(input.BetweenLast(after, before), Is.EqualTo(expected));
    }

    [TestCase("the quick brown fox jumps over the lazy dog", "*", "+")]
    [TestCase("[a] bc [d]", "*", "+")]
    public void Should_ReturnOriginalValue_When_SubstringNotExists(string input, string after, string before)
    {
        Assert.That(input.BetweenLast(after, before), Is.EqualTo(input));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.BetweenLast("x", "y"), Is.EqualTo(string.Empty));
    }
}
