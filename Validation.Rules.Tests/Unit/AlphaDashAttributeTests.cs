using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class AlphaDashAttributeTests
{
    [TestCase("abc-123_")]
    [TestCase("abc")]
    [TestCase("123")]
    [TestCase("-")]
    [TestCase("_")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value)
    {
        AlphaDashAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("abc 123")]
    [TestCase("abc!")]
    [TestCase("abc@")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value)
    {
        AlphaDashAttribute attribute = new() { ErrorMessage = "The {0} must contain only letters, numbers, dashes and underscores." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must contain only letters, numbers, dashes and underscores."));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        AlphaDashAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        AlphaDashAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
