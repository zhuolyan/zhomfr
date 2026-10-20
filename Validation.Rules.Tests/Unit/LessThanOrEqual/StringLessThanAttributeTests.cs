using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThanOrEqual;

[TestFixture]
public class StringGreaterThanOrEqualAttributeTests
{
    [TestCase("test1")]
    [TestCase("Test1")]
    [TestCase("test")]
    [TestCase("Test")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        string                      value             = "test";
        GreaterThanOrEqualAttribute attribute         = new(nameof(ModelWithAnotherField<>.Second));
        ValidationContext           validationContext = new(new ModelWithAnotherField<string> { Second = value, First = value });

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("tes")]
    [TestCase("Tes")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        const string FIELD_VAL = "test";

        GreaterThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.First)) { ErrorMessage = "The field {0} must be greater than or equal {1}." };

        ValidationContext validationContext = new(new ModelWithAnotherField<string> { Second = FIELD_VAL, First = FIELD_VAL }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than or equal First."));
    }
}
