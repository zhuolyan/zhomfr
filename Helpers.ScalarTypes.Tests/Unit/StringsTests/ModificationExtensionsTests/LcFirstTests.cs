using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class LcFirstTests
{
    [TestCase("Test", "test")]
    [TestCase("TEST", "tEST")]
    [TestCase("test", "test")]
    [TestCase("tEST", "tEST")]
    public void Should_ReturnStringWithFirstCharInLowerCase_When_Called(string input, string expected)
    {
        Assert.That(input.LcFirst(), Is.EqualTo(expected));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.LcFirst(), Is.Empty);
    }
}
