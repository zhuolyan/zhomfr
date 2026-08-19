using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class DecimalAttributeTests
{
    [TestCase("1", 0)]
    [TestCase("1,1", 1)]
    [TestCase("1,0", 1)]
    [TestCase("1,01", 2)]
    [TestCase("1,00", 2)]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value, int count)
    {
        DecimalAttribute  attribute         = new(count);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("1,10")]
    [TestCase("1,10")]
    [TestCase("1,00001")]
    [TestCase("1,10000")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value)
    {
        DecimalAttribute  attribute         = new(2, 5);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("1")]
    [TestCase("1,1")]
    [TestCase("1,0")]
    [TestCase("1,01")]
    [TestCase("1,00")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrectAndMustBeConcrete(string value)
    {
        DecimalAttribute attribute = new(3) { ErrorMessage = "The {0} must has {1} decimal precision." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must has 3 decimal precision."));
    }

    [TestCase("1")]
    [TestCase("1,1")]
    [TestCase("1,0")]
    [TestCase("1,000001")]
    [TestCase("1,100000")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrectAndMustBeBetween(string value)
    {
        DecimalAttribute attribute = new(2, 5) { ErrorMessage = "The {0} must has decimal precision between {1} and {2}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(value), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must has decimal precision between 2 and 5."));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DecimalAttribute  attribute         = new(3);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult("some string", validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DecimalAttribute  attribute1        = new(3);
        DecimalAttribute  attribute2        = new(2, 5);
        ValidationContext validationContext = new(new());

        ValidationResult? result1 = attribute1.GetValidationResult(null, validationContext);
        ValidationResult? result2 = attribute2.GetValidationResult(null, validationContext);

        Assert.That(result1, Is.EqualTo(ValidationResult.Success));
        Assert.That(result2, Is.EqualTo(ValidationResult.Success));
    }
}
