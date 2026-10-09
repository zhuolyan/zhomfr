using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

public class DoesntStartWithAttributeTest
{
    [TestCase("Start End")]
    [TestCase(" start end ")]
    [TestCase("anything")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value)
    {
        DoesntStartWithAttribute attribute         = new("start");
        ValidationContext        validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect()
    {
        DoesntStartWithAttribute attribute = new("start") { ErrorMessage = "The {0} must not start with: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult("start end", validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must not start with: start."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DoesntStartWithAttribute attribute         = new("start");
        ValidationContext        validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DoesntStartWithAttribute attribute         = new("start");
        ValidationContext        validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(123, validationContext));
    }
}
