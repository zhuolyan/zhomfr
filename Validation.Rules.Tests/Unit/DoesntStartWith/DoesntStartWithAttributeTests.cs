using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.DoesntStartWith;

[TestFixture]
public class DoesntStartWithAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DoesntStartWithAttribute attribute         = new("test", "blah test");
        ValidationContext        validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DoesntStartWithAttribute attribute         = new("test", "blah test");
        ValidationContext        validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ThrowArgumentException_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            DoesntStartWithAttribute containsAttribute = new();
        });
    }

    [TestCase("Start End")]
    [TestCase(" start end ")]
    [TestCase("enything")]
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
        DoesntStartWithAttribute attribute = new("start", "not") { ErrorMessage = "The {0} must not start with: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult("start end", validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must not start with: \"start\", \"not\"."));
    }
}
