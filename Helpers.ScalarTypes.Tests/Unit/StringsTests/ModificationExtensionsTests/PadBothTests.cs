using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class PadBothTests
{
    [TestCase("user", "   user   ")]
    [TestCase("zip", "   zip    ")]
    public void Should_ReturnStringPadedToBothSidesWithSpaces_When_LengthIsBiggestOfStringLengthAndCharNotSet(string input, string expected)
    {
        Assert.That(input.PadBoth(10), Is.EqualTo(expected));
    }

    [TestCase("user", "+-+user+-+")]
    [TestCase("zip", "+-+zip+-+-")]
    public void Should_ReturnPadedToBothSidesWithCustomChar_When_LengthIsBiggestOfStringLengthAndCharSet(string input, string expected)
    {
        Assert.That(input.PadBoth(10, "+-"), Is.EqualTo(expected));
    }

    [TestCase("user")]
    [TestCase("zip")]
    public void Should_ReturnSameString_When_LengthIsLessOrEqualOfStringLength(string input)
    {
        Assert.That(input.PadBoth(3), Is.EqualTo(input));
    }

    [Test]
    public void Should_ReturnOnlyStarsContainedString_When_Empty()
    {
        Assert.That(string.Empty.PadBoth(10, "*"), Is.EqualTo("**********"));
    }
}
