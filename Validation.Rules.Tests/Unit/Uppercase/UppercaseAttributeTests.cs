using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Uppercase;

[TestFixture]
public class UppercaseAttributeTests
{
    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        UppercaseAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        UppercaseAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("ABS123!@#")]
    [TestCase("ABS TEST")]
    [TestCase("ABS~TEST")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string input)
    {
        UppercaseAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("ABc123!@#")]
    [TestCase("ABs TEST")]
    [TestCase("ABs~TESt")]
    public void Should_ReturnValidationError_When_ValueIsInvalid(string inout)
    {
        UppercaseAttribute attribute = new() { ErrorMessage = "The {0} must contain only uppercase characters." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(inout, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must contain only uppercase characters."));
    }
}
