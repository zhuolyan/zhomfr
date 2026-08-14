using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class SubstringReplaceTests
{
    [TestCase(":", 2, 1, "12:456")]
    [TestCase(" of ", 3, 2, "123 of 6")]
    public void Should_ReplaceSubstring_When_LenghtIsBiggestOfZero(string replacement, int start, int length, string expected)
    {
        Assert.That(StubValues.SUBSTRING_REPLACE_VALUE.SubstringReplace(replacement, start, length), Is.EqualTo(expected));
    }

    [TestCase(":", 2, "12:")]
    [TestCase(" of ", 3, "123 of ")]
    public void Should_ReplaceFromStartIndexToEnd_When_LenghtIsEmpty(string replacement, int start, string expected)
    {
        Assert.That(StubValues.SUBSTRING_REPLACE_VALUE.SubstringReplace(replacement, start), Is.EqualTo(expected));
    }

    [TestCase(":", 2, "12:3456")]
    [TestCase(" of ", 3, "123 of 456")]
    public void Should_InputDataOnStartIndex_When_LenghtIsZero(string replacement, int start, string expected)
    {
        Assert.That(StubValues.SUBSTRING_REPLACE_VALUE.SubstringReplace(replacement, start, 0), Is.EqualTo(expected));
    }

    [TestCase(-1)]
    [TestCase(7)]
    public void Should_ReturnOriginalValue_When_IncorrectStart(int start)
    {
        Assert.That(StubValues.SUBSTRING_REPLACE_VALUE.SubstringReplace("test", start), Is.EqualTo(StubValues.SUBSTRING_REPLACE_VALUE));
    }
}
