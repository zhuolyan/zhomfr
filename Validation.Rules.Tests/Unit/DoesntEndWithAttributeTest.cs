using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class DoesntEndWithAttributeTest
{
    [TestCase("start End")]
    [TestCase("start end ")]
    [TestCase("anything")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value)
    {
        DoesntEndWithAttribute attribute         = new("end");
        ValidationContext      validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect()
    {
        DoesntEndWithAttribute attribute = new("end") { ErrorMessage = "The {0} must not end with: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult("some end", validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must not end with: end."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DoesntEndWithAttribute attribute         = new("end");
        ValidationContext      validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DoesntEndWithAttribute attribute         = new("end");
        ValidationContext      validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(123, validationContext));
    }
}
