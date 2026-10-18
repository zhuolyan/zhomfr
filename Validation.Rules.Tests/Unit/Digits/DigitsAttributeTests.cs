using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Digits;

[TestFixture]
public class DigitsAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DigitsAttribute   attribute         = new(1);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DigitsAttribute   attribute         = new(1);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCase("0.00")]
    [TestCase("0.01")]
    [TestCase("1.00")]
    [TestCase("1.01")]
    [TestCase("10.0")]
    [TestCase("10.1")]
    public void Should_ReturnSuccess_When_EqualExpectedAndValueIsCorrect(string value)
    {
        DigitsAttribute   attribute         = new(3);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("0.0")]
    [TestCase("0.00")]
    [TestCase("0.000")]
    [TestCase("0.1")]
    [TestCase("0.01")]
    [TestCase("0.001")]
    [TestCase("1.0")]
    [TestCase("1.00")]
    [TestCase("1.000")]
    [TestCase("1.1")]
    [TestCase("1.01")]
    [TestCase("1.001")]
    [TestCase("10")]
    [TestCase("10.0")]
    [TestCase("10.00")]
    [TestCase("10.1")]
    [TestCase("10.01")]
    public void Should_ReturnSuccess_When_BetweenExpectedAndValueIsCorrect(string value)
    {
        DigitsAttribute   attribute         = new(2, 4);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("0.0")]
    [TestCase("0.000")]
    [TestCase("0.1")]
    [TestCase("0.001")]
    [TestCase("1.0")]
    [TestCase("1.000")]
    [TestCase("1.1")]
    [TestCase("1.001")]
    [TestCase("10")]
    [TestCase("10.00")]
    [TestCase("10.01")]
    public void Should_ReturnValidationError_When_EqualExpectedAndValueIsIncorrect(string value)
    {
        DigitsAttribute   attribute         = new(3) { ErrorMessage    = "The {0} must has equal {1} digits." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must has equal 3 digits."));
    }

    [TestCase("0.0")]
    [TestCase("0.0000")]
    [TestCase("0.1")]
    [TestCase("0.0001")]
    [TestCase("1.0")]
    [TestCase("1.0000")]
    [TestCase("1.1")]
    [TestCase("1.0001")]
    [TestCase("10.000")]
    [TestCase("10")]
    [TestCase("10.001")]
    public void Should_ReturnValidationError_When_BetweenExpectedAndValueIsIncorrect(string value)
    {
        DigitsAttribute   attribute         = new(3, 4) { ErrorMessage = "The {0} must has between {1} and {2} digits." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must has between 3 and 4 digits."));
    }
}
