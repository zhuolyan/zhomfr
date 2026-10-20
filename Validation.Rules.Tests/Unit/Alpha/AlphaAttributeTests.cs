using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Alpha;

[TestFixture]
public class AlphaAttributeTests
{
    [TestCase("abc")]
    [TestCase("ABC")]
    [TestCase("abcABC")]
    [TestCase("abcABC")]
    [TestCase("світ")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string input)
    {
        AlphaAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("abc1")]
    [TestCase("abc-")]
    [TestCase("abc ")]
    [TestCase("світ1")]
    public void Should_ReturnValidationError_When_ValueIsInvalid(string input)
    {
        AlphaAttribute attribute = new() { ErrorMessage = "The value of {0} must contain only letters." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The value of MyProp must contain only letters."));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
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
