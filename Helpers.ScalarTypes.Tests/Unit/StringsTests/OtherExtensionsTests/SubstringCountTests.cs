using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.OtherExtensionsTests;

[TestFixture]
public class SubstringCountTests
{
    [TestCase("the", 2)]
    [TestCase("o", 4)]
    [TestCase(" ", 8)]
    public void Should_ReturnCountOfSubstring_When_SubstringExists(string search, int expected)
    {
        Assert.That(StubValues.VALUE.SubstringCount(search), Is.EqualTo(expected));
    }

    [TestCase("something else")]
    [TestCase("")]
    public void Should_ReturnZero_When_SubstringNotExistOrEmpty(string search)
    {
        Assert.That(StubValues.VALUE.SubstringCount(search), Is.EqualTo(0));
    }

    [Test]
    public void Should_ReturnZero_When_ValueIsEmpty()
    {
        Assert.That(string.Empty.SubstringCount("something"), Is.EqualTo(0));
    }
}
