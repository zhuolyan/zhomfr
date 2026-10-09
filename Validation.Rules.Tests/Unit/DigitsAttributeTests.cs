using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class DigitsAttributeTests
{
    [TestCase("0,00")]
    [TestCase("0,01")]
    [TestCase("100")]
    [TestCase("0,100")]
    [TestCase("1,000")]
    [TestCase("1234")]
    [TestCase("123,4")]
    [TestCase("12,34")]
    [TestCase("1,234")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value)
    {
        DigitsAttribute   attribute         = new(3, 4);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("0,00", 3)]
    [TestCase("0,01", 3)]
    [TestCase("100", 3)]
    [TestCase("0,100", 4)]
    [TestCase("1,000", 4)]
    [TestCase("1234", 4)]
    [TestCase("123,4", 4)]
    [TestCase("12,34", 4)]
    [TestCase("1,234", 4)]
    public void Should_ReturnSuccess_When_ValueIsExactSize(string value, int size)
    {
        DigitsAttribute   attribute         = new(size);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("10")]
    [TestCase("0,1")]
    [TestCase("1,0")]
    [TestCase("1,0000")]
    [TestCase("0,0000")]
    [TestCase("12345")]
    [TestCase("1234,5")]
    [TestCase("123,45")]
    [TestCase("12,345")]
    [TestCase("1,2345")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value)
    {
        DigitsAttribute attribute = new(3, 4) { ErrorMessage = "The {0} must have between {1} and {2} digits." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must have between 3 and 4 digits."));
    }

    [TestCase("10", 2)]
    [TestCase("0,1", 2)]
    [TestCase("1,0", 2)]
    [TestCase("1,0000", 5)]
    [TestCase("0,0000", 5)]
    [TestCase("12345", 5)]
    [TestCase("1234,5", 5)]
    [TestCase("123,45", 5)]
    [TestCase("12,345", 5)]
    [TestCase("1,2345", 5)]
    public void Should_ReturnValidationError_When_ValueIsNotExactSize(string value, int size)
    {
        DigitsAttribute attribute = new(size) { ErrorMessage = "The {0} must have exact {1} digits." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The MyProp must have exact {size} digits."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DigitsAttribute   attribute         = new(2, 4);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DigitsAttribute   attribute         = new(2, 4);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult("something", validationContext));
    }
}
