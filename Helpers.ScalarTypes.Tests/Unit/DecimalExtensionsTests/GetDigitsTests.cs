namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.DecimalExtensionsTests;

[TestFixture]
public class GetDigitsTests
{
    [Test]
    [TestCase(0, 1)]
    [TestCase(123, 3)]
    [TestCase(123.45, 5)]
    [TestCase(-123, 3)]
    [TestCase(0.123, 4)]
    public void Should_ReturnCorrectCount_When_CalledWithDouble(decimal number, int expected)
    {
        int result = number.GetDigits();

        Assert.That(result, Is.EqualTo(expected));
    }
}
