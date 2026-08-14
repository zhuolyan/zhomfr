using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ToKebabCaseTests
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
    public void Should_ReturnKebabCase_When_Called(string input)
    {
        Assert.That(input.ToKebabCase(), Is.EqualTo("foo-bar"));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.ToKebabCase(), Is.Empty);
    }
}
