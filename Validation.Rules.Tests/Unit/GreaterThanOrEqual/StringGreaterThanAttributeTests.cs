using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThanOrEqual;

[TestFixture]
public class StringGreaterThanOrEqualAttributeTests
{
    [TestCase("tes")]
    [TestCase("Tes")]
    [TestCase("test")]
    [TestCase("Test")]
    public void Should_ReturnSuccessResult_When_IsCorrectString(string value)
    {
        GreaterThanOrEqualAttribute attribute         = new("3");
        ValidationContext           validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("te")]
    [TestCase("Te")]
    public void Should_ReturnValidationError_When_IsIncorrectString(string value)
    {
        GreaterThanOrEqualAttribute attribute         = new("3") { ErrorMessage  = "The field {0} must be greater than or equal {1}." };
        ValidationContext           validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than or equal 3."));
    }

    [TestCase("test1")]
    [TestCase("Test1")]
    [TestCase("test")]
    [TestCase("Test")]
    public void Should_ReturnSuccessResult_When_IsCorrectField(string value)
    {
        string                      fieldVal          = "test";
        GreaterThanOrEqualAttribute attribute         = new(nameof(ModelWithAnotherField<>.AnotherField));
        ValidationContext           validationContext = new(new ModelWithAnotherField<string> { AnotherField = fieldVal, AnotherField2 = fieldVal });

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("tes")]
    [TestCase("Tes")]
    public void Should_ReturnValidationError_When_IsIncorrectField(string value)
    {
        const string FIELD_VAL = "test";

        GreaterThanOrEqualAttribute attribute
            = new(nameof(ModelWithAnotherField<>.AnotherField2)) { ErrorMessage = "The field {0} must be greater than or equal {1}." };

        ValidationContext validationContext
            = new(new ModelWithAnotherField<string> { AnotherField = FIELD_VAL, AnotherField2 = FIELD_VAL }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than or equal AnotherField2."));
    }
}
