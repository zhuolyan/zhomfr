using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class StartsWithAttributeTest
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        StartsWithAttribute attribute         = new("start");
        ValidationContext   validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult("start end", validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    [TestCase("Start End")]
    [TestCase(" start end ")]
    [TestCase("enything")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value)
    {
        StartsWithAttribute attribute = new("start") { ErrorMessage = "The {0} must start with: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must start with: start."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        StartsWithAttribute attribute         = new("start");
        ValidationContext   validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        StartsWithAttribute attribute         = new("start");
        ValidationContext   validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(123, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
    }
}
