using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class UpperCaseAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        UpperCaseAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult("TEST", validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    [TestCase("tEST")]
    [TestCase("test")]
    [TestCase("TESt")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value)
    {
        UpperCaseAttribute attribute = new() { ErrorMessage = "The {0} must contain only uppercase characters." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must contain only uppercase characters."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        UpperCaseAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        UpperCaseAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(1, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The field Object is invalid."));
    }
}
