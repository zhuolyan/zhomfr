using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.SubstringExtensionsTests;

[TestFixture]
public class LimitTests
{
    [TestCase("", "the quick ")]
    [TestCase("...", "the qui...")]
    public void Should_ReturnSubstringWithEndingAndResultLenghtEqualLimit_When_InputLengthMoreThanLimit(string end, string expected)
    {
        Assert.That(StubValues.VALUE.Limit(10, end), Is.EqualTo(expected));
    }

    [TestCase("...", "the...")]
    public void Should_ReturnSubstringWithEndingAfterLastWholeWord_When_InputLengthMoreThanLimit(string end, string expected)
    {
        Assert.That(StubValues.VALUE.Limit(10, end, true), Is.EqualTo(expected));
    }

    [TestCase("the quick", "", "the quick")]
    [TestCase("Amogus", "...", "Amogus")]
    public void Should_ReturnOriginalString_When_InputLengthLessOrEqualThanLimit(string input, string end, string expected)
    {
        Assert.That(input.Limit(10, end), Is.EqualTo(expected));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.Limit(10), Is.EqualTo(string.Empty));
    }
}
