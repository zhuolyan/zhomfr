using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ExcerptTests
{
    [TestCase("my", "...is my na...")]
    [TestCase("is", "This is...")]
    [TestCase("na", "...my name")]
    public void Should_ReturnStringWithDotsOmission_When_OnlyRadiusSet(string search, string result)
    {
        Assert.That(StubValues.EXCERPT_VALUE.Excerpt(search, 3), Is.EqualTo(result));
    }

    [TestCase("my", "{...}is my na{...}")]
    [TestCase("is", "This is{...}")]
    [TestCase("na", "{...}my name")]
    public void Should_ReturnStringWithCustomOmission_When_RadiusAndOmissionSet(string search, string result)
    {
        Assert.That(StubValues.EXCERPT_VALUE.Excerpt(search, 3, "{...}"), Is.EqualTo(result));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.Excerpt("test", 3), Is.Empty);
    }
}
