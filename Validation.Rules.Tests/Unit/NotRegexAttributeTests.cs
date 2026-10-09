using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class NotRegexAttributeTests
{
    [TestCase("abc", "^[0-9]+$")]
    [TestCase("123", "^[a-z]+$")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value, string regex)
    {
        NotRegexAttribute attribute         = new(regex);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("12345", "^[0-9]+$")]
    [TestCase("abc", "^[a-z]+$")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value, string regex)
    {
        NotRegexAttribute attribute = new(regex) { ErrorMessage = "The {0} must not match the regular expression: \"{1}\"." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The MyProp must not match the regular expression: \"{regex}\"."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        NotRegexAttribute attribute         = new("^[a-z]+$");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        NotRegexAttribute attribute         = new("^[a-z]+$");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(1, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The field Object is invalid."));
    }
}
