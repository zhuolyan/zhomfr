using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ToCamelCaseTests
{
    [TestCase("Foo Bar")]
    [TestCase("foo bar")]
    [TestCase("Foo   Bar")]
    [TestCase("FooBar")]
    [TestCase("fooBar")]
    [TestCase("FOOBar")]
    [TestCase("foo-bar")]
    [TestCase("foo_bar")]
    [TestCase("FOO_BAR")]
    [TestCase("FOO-BAR")]
    public void Should_ReturnCamelCase_When_Called(string input)
    {
        Assert.That(input.ToCamelCase(), Is.EqualTo("fooBar"));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.ToCamelCase(), Is.Empty);
    }
}
