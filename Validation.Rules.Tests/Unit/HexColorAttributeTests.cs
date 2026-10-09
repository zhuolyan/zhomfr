using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class HexColorAttributeTests
{
    [TestCase("#FFF")]
    [TestCase("#FFFFFF")]
    [TestCase("#ABCD")]
    [TestCase("#12345678")]
    public void Should_ReturnSuccess_When_StringIsCorrect(string value)
    {
        HexColorAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("FFF")]
    [TestCase("#GGGGGG")]
    [TestCase("#12345")]
    [TestCase("#1234567")]
    [TestCase("blue")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value)
    {
        HexColorAttribute attribute = new() { ErrorMessage = "The {0} must be hex color." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be hex color."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        HexColorAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        HexColorAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(123, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
    }
}
