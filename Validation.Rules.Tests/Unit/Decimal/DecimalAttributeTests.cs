using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Decimal;

[TestFixture]
public class DecimalAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DecimalAttribute  attribute         = new(1);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        DecimalAttribute  attribute         = new(1);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCase("0.000")]
    [TestCase("0.001")]
    [TestCase("1.000")]
    [TestCase("1.001")]
    [TestCase("10.000")]
    [TestCase("10.001")]
    public void Should_ReturnSuccess_When_EqualExpectedAndValueIsCorrect(string input)
    {
        DecimalAttribute  attribute         = new(3);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("0.00")]
    [TestCase("0.000")]
    [TestCase("0.0000")]
    [TestCase("0.01")]
    [TestCase("0.001")]
    [TestCase("0.0001")]
    [TestCase("1.00")]
    [TestCase("1.000")]
    [TestCase("1.0000")]
    [TestCase("1.01")]
    [TestCase("1.001")]
    [TestCase("1.0001")]
    [TestCase("10.00")]
    [TestCase("10.000")]
    [TestCase("10.0000")]
    [TestCase("10.01")]
    [TestCase("10.001")]
    [TestCase("10.0001")]
    public void Should_ReturnSuccess_When_BetweenExpectedAndValueIsCorrect(string input)
    {
        DecimalAttribute  attribute         = new(2, 4);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("0.00")]
    [TestCase("0.0000")]
    [TestCase("0.01")]
    [TestCase("0.0001")]
    [TestCase("1.00")]
    [TestCase("1.0000")]
    [TestCase("1.01")]
    [TestCase("1.0001")]
    [TestCase("10.00")]
    [TestCase("10.0000")]
    [TestCase("10.01")]
    [TestCase("10.0001")]
    public void Should_ReturnValidationError_When_EqualExpectedAndValueIsInvalid(string input)
    {
        DecimalAttribute  attribute         = new(3) { ErrorMessage    = "The {0} must has equal {1} numbers of decimal places." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must has equal 3 numbers of decimal places."));
    }

    [TestCase("0.0")]
    [TestCase("0.00000")]
    [TestCase("0.1")]
    [TestCase("0.00001")]
    [TestCase("1.0")]
    [TestCase("1.00000")]
    [TestCase("1.1")]
    [TestCase("1.00001")]
    [TestCase("10.0")]
    [TestCase("10.00000")]
    [TestCase("10.1")]
    [TestCase("10.00001")]
    public void Should_ReturnValidationError_When_BetweenExpectedAndValueIsInvalid(string input)
    {
        DecimalAttribute  attribute         = new(2, 4) { ErrorMessage = "The {0} must has between {1} and {2} numbers of decimal places." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must has between 2 and 4 numbers of decimal places."));
    }
}
