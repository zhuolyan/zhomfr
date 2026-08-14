using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ToSnakeCaseTests
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
    public void Should_ReturnStringInSnakeCase_When_Called(string input)
    {
        Assert.That(input.ToSnakeCase(), Is.EqualTo("foo_bar"));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.ToSnakeCase(), Is.Empty);
    }
}
