namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.DecimalExtensionsTests;

[TestFixture]
public class GetDigitsBeforeSeparatorTests
{
    [Test]
    [TestCase(0, 1)]
    [TestCase(5, 1)]
    [TestCase(10, 2)]
    [TestCase(123, 3)]
    [TestCase(123.45, 3)]
    [TestCase(-123, 3)]
    [TestCase(0.5, 1)]
    public void Should_ReturnCorrectCount_When_CalledWithDouble(decimal number, int expected)
    {
        int result = number.GetDigitsBeforeSeparator();

        Assert.That(result, Is.EqualTo(expected));
    }
}
