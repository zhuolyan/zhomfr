using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ToBase64Tests
{
    [TestCase("test", "dGVzdA==")]
    [TestCase("Helo world!!", "SGVsbyB3b3JsZCEh")]
    [TestCase("Привіт світ", "0J/RgNC40LLRltGCINGB0LLRltGC")]
    public void Should_ReturnBase64String_When_Called(string input, string expected)
    {
        Assert.That(input.ToBase64(), Is.EqualTo(expected));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.ToBase64(), Is.Empty);
    }
}
