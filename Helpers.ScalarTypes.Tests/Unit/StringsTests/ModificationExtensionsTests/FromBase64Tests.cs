using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class FromBase64Tests
{
    [TestCase("aGVsbG8gd29ybGQ=", "hello world")]
    [TestCase("dGVzdGVk", "tested")]
    public void Should_ReturnDecodedString_When_InputIsBase64(string input, string expected)
    {
        Assert.That(input.FromBase64(), Is.EqualTo(expected));
    }

    [TestCase("")]
    [TestCase("hello world")]
    public void Should_ReturnEmptyString_When_Empty(string value)
    {
        Assert.That(value.FromBase64(), Is.Null);
    }
}
