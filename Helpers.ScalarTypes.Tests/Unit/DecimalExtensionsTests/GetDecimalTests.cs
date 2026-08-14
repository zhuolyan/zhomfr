namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.DecimalExtensionsTests;

[TestFixture]
public class GetDecimalTests
{
    [Test]
    [TestCase(0, 0)]
    [TestCase(123, 0)]
    [TestCase(1.2, 1)]
    [TestCase(1.23, 2)]
    [TestCase(12.345, 3)]
    [TestCase(-1.23, 2)]
    public void Should_ReturnCorrectCount_When_CalledWithDouble(decimal number, int expected)
    {
        int result = number.GetDecimal();

        Assert.That(result, Is.EqualTo(expected));
    }
}
