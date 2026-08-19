using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class AlphaAttributeTest
{
    [TestCase("abc")]
    [TestCase("ABC")]
    [TestCase("abcABC")]
    [TestCase("abcABC")]
    [TestCase("світ")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value)
    {
        AlphaAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("abc1")]
    [TestCase("abc-")]
    [TestCase("abc ")]
    [TestCase("світ1")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value)
    {
        AlphaAttribute attribute = new() { ErrorMessage = "The value of {0} must contain only letters." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The value of MyProp must contain only letters."));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        AlphaAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        AlphaAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
