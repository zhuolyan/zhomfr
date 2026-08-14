using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class UcFirstTests
{
    [TestCase("Test", "Test")]
    [TestCase("TEST", "TEST")]
    [TestCase("test", "Test")]
    [TestCase("tEST", "TEST")]
    public void Should_ReturnStringWithFirstCharInUpperCase_When_Called(string input, string expected)
    {
        Assert.That(input.UcFirst(), Is.EqualTo(expected));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.UcFirst(), Is.Empty);
    }
}
