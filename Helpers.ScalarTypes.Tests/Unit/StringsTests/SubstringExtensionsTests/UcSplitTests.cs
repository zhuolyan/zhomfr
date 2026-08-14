using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.SubstringExtensionsTests;

[TestFixture]
public class UcSplitTests
{
    [TestCaseSource(typeof(StubValues), nameof(StubValues.UcSplitStub))]
    public void Should_ReturnStringListWithStringWhatStartUpperCaseAndEndBeforeNextUpperCase_When_Called(UcSplitStubValue value)
    {
        Assert.That(value.Input.UcSplit(), Is.EqualTo(value.Expected));
    }
}
