using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class SquishTests
{
    [Test]
    public void Should_RemoveExtraWhitespace_When_Called()
    {
        const string VALUE = "    The    quick   brown fox    ";

        string result = VALUE.Squish();

        Assert.That(result, Is.EqualTo("The quick brown fox"));
    }

    [Test]
    public void Should_HandleNewLinesAndTabs_When_Present()
    {
        const string VALUE = "The\nquick\t\tbrown\r\nfox";
        Assert.That(VALUE.Squish(), Is.EqualTo("The quick brown fox"));
    }

    [Test]
    public void Should_ReturnEmptyString_When_ValueIsEmptyString()
    {
        Assert.That(string.Empty.Squish(), Is.EqualTo(string.Empty));
    }
}
