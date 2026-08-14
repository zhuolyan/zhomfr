using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class ReverseTests
{
    [Test]
    public void Should_ReverseString_When_Called()
    {
        const string VALUE = "ABCDEF";

        Assert.That(VALUE.Reverse(), Is.EqualTo("FEDCBA"));
    }

    [Test]
    public void Should_HandleEmptyString_When_InputIsEmpty()
    {
        Assert.That(string.Empty.Reverse(), Is.EqualTo(string.Empty));
    }
}
