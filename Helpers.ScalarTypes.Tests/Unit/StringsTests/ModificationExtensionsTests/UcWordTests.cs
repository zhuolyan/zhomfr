using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

public class UcWordTests
{
    [TestCase("the quick brown fox jumps over the lazy dog!")]
    [TestCase("The Quick Brown Fox Jumps Over The Lazy Dog!")]
    public void Should_ReturnStringWithEveryWordFirstCharInUpperCase_When_Called(string input)
    {
        Assert.That(input.UcWord(), Is.EqualTo("The Quick Brown Fox Jumps Over The Lazy Dog!"));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.UcWord(), Is.Empty);
    }
}
