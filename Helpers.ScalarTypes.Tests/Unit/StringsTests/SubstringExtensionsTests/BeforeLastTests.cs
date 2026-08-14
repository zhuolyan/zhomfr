using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.SubstringExtensionsTests;

[TestFixture]
public class BeforeLastTests
{
    [TestCase("quick ", "the ")]
    [TestCase("the ", "the quick brown fox jumps over ")]
    public void Should_ReturnSubstringBeforeFirstFind_When_SubstringExists(string search, string expected)
    {
        Assert.That(StubValues.VALUE.BeforeLast(search), Is.EqualTo(expected));
    }

    [TestCase("cat")]
    [TestCase("lord")]
    public void Should_ReturnOriginalValue_When_SubstringNotExists(string search)
    {
        Assert.That(StubValues.VALUE.BeforeLast(search), Is.EqualTo(StubValues.VALUE));
    }

    [Test]
    public void Should_ReturnEmpty_When_SubstringExistsAtEndOfInputString()
    {
        Assert.That(StubValues.VALUE.BeforeLast("the quick"), Is.Empty);
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.BeforeLast("hello "), Is.EqualTo(string.Empty));
    }
}
