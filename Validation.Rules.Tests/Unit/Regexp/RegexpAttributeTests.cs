using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Regexp;

[TestFixture]
public class RegexpAttributeTests
{
    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        ModelWithAnotherField<int> model             = new() { Second = 32, First = 32 };
        RegexpAttribute            attribute         = new("^[0-9]+$");
        ValidationContext          validationContext = new(model);

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(model, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        RegexpAttribute   attribute         = new("^[0-9]+$");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void ShouldThrow_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            RegexpAttribute attribute = new("");
        });
    }

    [TestCase("12345", "^[0-9]+$")]
    [TestCase("abc", "^[a-z]+$")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value, string regex)
    {
        RegexpAttribute   attribute         = new(regex);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("abc", "^[0-9]+$")]
    [TestCase("123", "^[a-z]+$")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value, string regex)
    {
        RegexpAttribute attribute = new(regex) { ErrorMessage = "The {0} must match the regular expression: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The MyProp must match the regular expression: \"{regex}\"."));
    }
}
