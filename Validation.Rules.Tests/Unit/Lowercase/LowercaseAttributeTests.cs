using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Lowercase;

[TestFixture]
public class LowercaseAttribute
{
    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        Rules.LowercaseAttribute    attribute         = new();
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
    [TestCase("abc123!@#")]
    [TestCase("abs test")]
    [TestCase("abs~test")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string input)
    {
        AsciiAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("Abc123!@#")]
    [TestCase("abs test")]
    [TestCase("abs~Test")]
    public void Should_ReturnValidationError_When_ValueIsInvalid(string inout)
    {
        AsciiAttribute attribute = new() { ErrorMessage = "The {0} must contain only ASCII characters." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(inout, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must contain only ASCII characters."));
    }
}
