using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.OtherExtensionsTests;

[TestFixture]
public class InitialsTests
{
    [TestCase("test user", "tu")]
    [TestCase("test User", "tU")]
    [TestCase("Test User", "TU")]
    [TestCase("Test User Example", "TUE")]
    public void Should_ReturnInitialsInOriginalCase_When_CapitalizedIsFalse(string input, string expected)
    {
        Assert.That(input.Initials(false), Is.EqualTo(expected));
    }

    [TestCase("test user", "TU")]
    [TestCase("test User", "TU")]
    [TestCase("Test User", "TU")]
    [TestCase("Test User Example", "TUE")]
    public void Should_ReturnInitialsInUpperCase_When_CapitalizedIsTrue(string input, string expected)
    {
        Assert.That(input.Initials(), Is.EqualTo(expected));
    }

    [Test]
    public void Should_ReturnEmptyString_When_Empty()
    {
        Assert.That(string.Empty.Initials(), Is.Empty);
    }
}
