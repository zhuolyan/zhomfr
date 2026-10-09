using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class LowerCaseAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        LowerCaseAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult("test", validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("Test")]
    [TestCase("TEST")]
    [TestCase("tesT")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value)
    {
        LowerCaseAttribute attribute = new() { ErrorMessage = "The {0} must contain only lowercase characters." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must contain only lowercase characters."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        LowerCaseAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowArgumentException_Then_FieldsAreDifferentTypes()
    {
        LowerCaseAttribute attribute = new();

        ValidationContext validationContext = new(new());

        Assert.Throws<FiledNotExistsException>(() => attribute.GetValidationResult(2, validationContext));
    }
}
