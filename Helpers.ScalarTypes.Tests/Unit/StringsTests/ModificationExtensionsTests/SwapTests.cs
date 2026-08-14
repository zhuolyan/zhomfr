using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class SwapTests
{
    [TestCaseSource(typeof(StubValues), nameof(StubValues.SwapStub))]
    public void Should_ReplaceSubstringsByValue_When_Called(SwapStubValue value)
    {
        Assert.That(value.Input.Swap(value.Replacements), Is.EqualTo(value.Expected));
    }

    [TestCaseSource(typeof(StubValues), nameof(StubValues.SwapStub))]
    public void Should_ReturnEmptyString_When_Empty(SwapStubValue value)
    {
        Assert.That(string.Empty.Swap(value.Replacements), Is.Empty);
    }
}
