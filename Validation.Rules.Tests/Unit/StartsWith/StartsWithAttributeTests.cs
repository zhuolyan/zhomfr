using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.StartsWith;

[TestFixture]
public class StartsWithAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        StartsWithAttribute attribute         = new("test", "blah test");
        ValidationContext   validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        StartsWithAttribute attribute         = new("test", "blah test");
        ValidationContext   validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void ShouldThrow_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            StartsWithAttribute containsAttribute = new();
        });
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        StartsWithAttribute attribute         = new("start");
        ValidationContext   validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult("start end", validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("Start End")]
    [TestCase(" start end ")]
    [TestCase("anything")]
    public void Should_ReturnValidationError_When_ValueIsInvalid(string input)
    {
        StartsWithAttribute attribute = new("start", "not") { ErrorMessage = "The {0} must start with: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must start with: \"start\", \"not\"."));
    }
}
