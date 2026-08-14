using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.SubstringExtensionsTests;

[TestFixture]
public class BeforeTests
{
    [TestCase("quick ", "the ")]
    [TestCase("the ", "")]
    public void Should_ReturnSubstringBeforeFirstFind_When_SubstringExists(string search, string expected)
    {
        Assert.That(StubValues.VALUE.Before(search), Is.EqualTo(expected));
    }

    [TestCase("cat")]
    [TestCase("lord")]
    public void Should_ReturnOriginalValue_When_SubstringNotExists(string search)
    {
        Assert.That(StubValues.VALUE.Before(search), Is.EqualTo(StubValues.VALUE));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.Before("hello "), Is.EqualTo(string.Empty));
    }
}
