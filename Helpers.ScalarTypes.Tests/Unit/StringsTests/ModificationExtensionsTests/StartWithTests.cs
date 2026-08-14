using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class StartWithTests
{
    [Test]
    public void Should_AddPrefix_When_ValueDoesNotStartWithIt()
    {
        Assert.That("world".StartWith("hello "), Is.EqualTo("hello world"));
    }

    [Test]
    public void Should_NotAddPrefix_When_ValueAlreadyStartsWithIt()
    {
        const string VALUE = "hello world";

        Assert.That(VALUE.StartWith("hello "), Is.EqualTo(VALUE));
    }

    [Test]
    public void Should_ReturnOriginalValue_When_PrefixIsEmptyString()
    {
        const string VALUE = "world";

        Assert.That(VALUE.StartWith(string.Empty), Is.EqualTo(VALUE));
    }
}
