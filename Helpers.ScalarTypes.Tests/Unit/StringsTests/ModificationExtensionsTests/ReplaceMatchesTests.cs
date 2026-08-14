using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ReplaceMatchesTests
{
    [TestCase("Ціна: 100 грн, доставка: 50 грн", @"\d+", "0", "Ціна: 0 грн, доставка: 0 грн")]
    [TestCase("Cat, cat, CAT", "cat", "dog", "Cat, dog, CAT")]
    [TestCase("abcdef", @"\d+", "X", "abcdef")]
    [TestCase("one    two\tthree", @"\s+", " ", "one two three")]
    [TestCase("John Smith", @"(?<first>\w+) (?<last>\w+)", "${last}, ${first}", "Smith, John")]
    [TestCase("", "/.*", "test", "")]
    public void Should_ReturnExpectedResult_When_Called(string input, string pattern, string replacement, string expected)
    {
        Assert.That(input.ReplaceMatches(pattern, replacement), Is.EqualTo(expected));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.ReplaceMatches(@"\s+", " "), Is.Empty);
    }
}
