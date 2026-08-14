using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.OtherExtensionsTests;

[TestFixture]
public class WordCountTests
{
    [TestCase(StubValues.VALUE, 9)]
    [TestCase("something", 1)]
    [TestCase("", 0)]
    public void Should_ReturnCountOfWords_When_Called(string input, int expected)
    {
        Assert.That(input.WordCount(), Is.EqualTo(expected));
    }
}
