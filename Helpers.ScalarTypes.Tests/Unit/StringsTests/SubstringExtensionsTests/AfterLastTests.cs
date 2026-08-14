using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.SubstringExtensionsTests;

[TestFixture]
public class AfterLastTests
{
    [TestCase("quick ", "brown fox jumps over the lazy dog")]
    [TestCase("the ", "lazy dog")]
    [TestCase("dog", "")]
    public void Should_ReturnSubstringAfterFirstFind_When_SubstringExists(string search, string expected)
    {
        Assert.That(StubValues.VALUE.AfterLast(search), Is.EqualTo(expected));
    }

    [TestCase("cat")]
    [TestCase("lord")]
    public void Should_ReturnOriginalValue_When_SubstringNotExists(string search)
    {
        Assert.That(StubValues.VALUE.AfterLast(search), Is.EqualTo(StubValues.VALUE));
    }

    [Test]
    public void Should_ReturnEmpty_When_SubstringExistsAtEndOfInputString()
    {
        Assert.That(StubValues.VALUE.AfterLast("dog"), Is.Empty);
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.AfterLast("hello "), Is.EqualTo(string.Empty));
    }
}
