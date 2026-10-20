using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.EndWith;

[TestFixture]
public class EndWithAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        EndsWithAttribute attribute         = new("test", "blah test");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        EndsWithAttribute attribute         = new("test", "blah test");
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void ShouldThrow_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            EndsWithAttribute containsAttribute = new();
        });
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        EndsWithAttribute attribute         = new("end", "not");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult("start end", validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("Start End")]
    [TestCase(" start end ")]
    [TestCase("anything")]
    public void Should_ReturnValidationError_When_ValueIsInvalid(string input)
    {
        EndsWithAttribute attribute = new("end", "not") { ErrorMessage = "The {0} must end with one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must end with one of: \"end\", \"not\"."));
    }
}
