using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Ascii;

[TestFixture]
public class AsciiAttributeTests
{
    [TestCase("abc123!@#")]
    [TestCase(" ")]
    [TestCase("~")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string input)
    {
        AsciiAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("привіт")]
    [TestCase("🚀")]
    [TestCase("€")]
    public void Should_ReturnValidationError_When_ValueIsInvalid(string inout)
    {
        AsciiAttribute attribute = new() { ErrorMessage = "The {0} must contain only ASCII characters." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(inout, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must contain only ASCII characters."));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        AsciiAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        AsciiAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
