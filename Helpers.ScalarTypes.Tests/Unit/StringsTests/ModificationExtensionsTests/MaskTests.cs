using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.StringsTests.ModificationExtensionsTests;

[TestFixture]
public class MaskTests
{
    [TestCase(3, "use*****************")]
    [TestCase(4, "user****************")]
    [TestCase(5, "usern***************")]
    [TestCase(-3, "*****************com")]
    [TestCase(-4, "****************.com")]
    [TestCase(-5, "***************e.com")]
    public void Should_ReturnMaskedString_When_OnlyStartSet(int start, string expected)
    {
        Assert.That(StubValues.MASK_VALUE.Mask(start), Is.EqualTo(expected));
    }

    [TestCase(3, 3, "use**************com")]
    [TestCase(4, 4, "user************.com")]
    [TestCase(5, 5, "usern**********e.com")]
    [TestCase(-3, 3, "*****************com")]
    [TestCase(-4, 4, "****************.com")]
    [TestCase(-5, 5, "***************e.com")]
    [TestCase(-3, -3, "use**************com")]
    [TestCase(-4, -4, "user************.com")]
    [TestCase(-5, -5, "usern**********e.com")]
    public void Should_ReturnMaskedString_When_StartAndEndSet(int start, int end, string expected)
    {
        Assert.That(StubValues.MASK_VALUE.Mask(start, end), Is.EqualTo(expected));
    }

    [TestCase(3, "use+++++++++++++++++")]
    [TestCase(4, "user++++++++++++++++")]
    [TestCase(5, "usern+++++++++++++++")]
    [TestCase(-3, "+++++++++++++++++com")]
    [TestCase(-4, "++++++++++++++++.com")]
    [TestCase(-5, "+++++++++++++++e.com")]
    public void Should_ReturnMaskedWithCustomCharString_When_OnlyStartSet(int start, string expected)
    {
        Assert.That(StubValues.MASK_VALUE.Mask(start, mask: '+'), Is.EqualTo(expected));
    }

    [TestCase(3, 3, "use++++++++++++++com")]
    [TestCase(4, 4, "user++++++++++++.com")]
    [TestCase(5, 5, "usern++++++++++e.com")]
    [TestCase(-3, 3, "+++++++++++++++++com")]
    [TestCase(-4, 4, "++++++++++++++++.com")]
    [TestCase(-5, 5, "+++++++++++++++e.com")]
    [TestCase(-3, -3, "use++++++++++++++com")]
    [TestCase(-4, -4, "user++++++++++++.com")]
    [TestCase(-5, -5, "usern++++++++++e.com")]
    public void Should_ReturnMaskedWithCustomCharString_When_StartAndEndSet(int start, int end, string expected)
    {
        Assert.That(StubValues.MASK_VALUE.Mask(start, end, '+'), Is.EqualTo(expected));
    }

    [TestCase(4)]
    [TestCase(5)]
    [TestCase(-4)]
    [TestCase(-5)]
    public void Should_ReturnSameString_When_StringIsToShortAndOnlyStartSet(int start)
    {
        Assert.That(StubValues.MASK_SHORT_VALUE.Mask(start), Is.EqualTo(StubValues.MASK_SHORT_VALUE));
    }

    [TestCase(4, 4)]
    [TestCase(5, 5)]
    [TestCase(-4, 4)]
    [TestCase(-5, 5)]
    [TestCase(-4, -4)]
    [TestCase(-5, -5)]
    public void Should_ReturnSameString_When_StringIsToShortAndStartAndEndSet(int start, int end)
    {
        Assert.That(StubValues.MASK_SHORT_VALUE.Mask(start), Is.EqualTo(StubValues.MASK_SHORT_VALUE));
    }

    [Test]
    public void Should_ReturnEmpty_When_Empty()
    {
        Assert.That(string.Empty.Mask(10), Is.EqualTo(string.Empty));
    }
}
