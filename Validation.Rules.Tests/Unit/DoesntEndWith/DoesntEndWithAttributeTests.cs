using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.DoesntEndWith;

[TestFixture]
public class DoesntEndWithAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DoesntEndWithAttribute attribute         = new("test", "blah test");
        ValidationContext      validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        DoesntEndWithAttribute attribute         = new("test", "blah test");
        ValidationContext      validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void ShouldThrow_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            DoesntEndWithAttribute containsAttribute = new();
        });
    }

    [TestCase("Start End")]
    [TestCase(" start end ")]
    [TestCase("anything")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string input)
    {
        DoesntEndWithAttribute attribute         = new("end");
        ValidationContext      validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsInvalid()
    {
        DoesntEndWithAttribute attribute = new("end", "not") { ErrorMessage = "The {0} must not start with: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult("start end", validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must not start with: \"end\", \"not\"."));
    }
}
