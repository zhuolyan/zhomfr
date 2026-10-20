using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.LessThanOrEqual;

[TestFixture]
public class StringLessThanOrEqualAttributeTests
{
    [TestCase("tes")]
    [TestCase("Tes")]
    [TestCase("test")]
    [TestCase("Test")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        string                   value             = "test";
        LessThanOrEqualAttribute attribute         = new(nameof(ModelWithAnotherField<>.Second));
        ValidationContext        validationContext = new(new ModelWithAnotherField<string> { Second = value, First = value });

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("test1")]
    [TestCase("Test1")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        const string FIELD_VAL = "test";

        LessThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.First)) { ErrorMessage = "The field {0} must be less than or equal {1}." };

        ValidationContext validationContext = new(new ModelWithAnotherField<string> { Second = FIELD_VAL, First = FIELD_VAL }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be less than or equal First."));
    }
}
