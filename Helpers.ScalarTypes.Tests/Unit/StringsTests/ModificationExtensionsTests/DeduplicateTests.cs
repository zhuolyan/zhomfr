using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class DeduplicateTests
{
    [TestCase("testt", "t", "test")]
    [TestCase("teest", "e", "test")]
    [TestCase("test", "e", "test")]
    [TestCase("ttest", "t", "test")]
    [TestCase("blahblahblah", "blah", "blah")]
    [TestCase("it is blahblahblah", "blah", "it is blah")]
    [TestCase("blahblahblah whats what she sayid", "blah", "blah whats what she sayid")]
    public void Should_DeduplicateSpecifiedSubstrings_When_SearchProvided(string input, string search, string result)
    {
        Assert.That(input.Deduplicate(search), Is.EqualTo(result));
    }

    [TestCase("large   space", "large space")]
    [TestCase("  large   space", " large space")]
    [TestCase("  large   space    ", " large space ")]
    [TestCase("large   space    ", "large space ")]
    [TestCase("large space    ", "large space ")]
    [TestCase("large space", "large space")]
    public void Should_DeduplicateSpaces_When_SearchNotProvided(string input, string result)
    {
        Assert.That(input.Deduplicate(), Is.EqualTo(result));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.Deduplicate(), Is.Empty);
    }
}
