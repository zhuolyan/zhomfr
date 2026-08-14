using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.OtherExtensionsTests;

[TestFixture]
public class CharAtTests
{
    private const string VALUE = "Laravel";

    [Test]
    public void Should_ReturnCharacterAtIndex_When_IndexIsValid()
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(CharAtTests.VALUE.CharAt(0), Is.EqualTo('L'));
            Assert.That(CharAtTests.VALUE.CharAt(2), Is.EqualTo('r'));
        }
    }

    [Test]
    public void Should_ReturnNull_When_IndexIsOutOfBounds()
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(CharAtTests.VALUE.CharAt(10), Is.Null);
            Assert.That(CharAtTests.VALUE.CharAt(-1), Is.Null);
        }
    }
}
