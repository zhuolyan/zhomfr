using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThan;

[TestFixture]
public class StringGreaterThanAttributeTests
{
    [TestCase("test")]
    [TestCase("Test")]
    public void Should_ReturnSuccessResult_When_IsCorrectString(string value)
    {
        GreaterThanAttribute attribute         = new("3");
        ValidationContext    validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("test")]
    [TestCase("tes")]
    public void Should_ReturnValidationError_When_IsIncorrectString(string value)
    {
        GreaterThanAttribute attribute         = new("4") { ErrorMessage  = "The field {0} must be greater than {1}." };
        ValidationContext    validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than 4."));
    }

    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrectField()
    {
        string               fieldVal          = "test";
        GreaterThanAttribute attribute         = new(nameof(ModelWithAnotherField<>.AnotherField));
        ValidationContext    validationContext = new(new ModelWithAnotherField<string> { AnotherField = fieldVal, AnotherField2 = fieldVal });

        ValidationResult? result = attribute.GetValidationResult("test1", validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("test")]
    [TestCase("tes")]
    public void Should_ReturnValidationError_When_IsIncorrectField(string value)
    {
        string               fieldVal  = "test";
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField2)) { ErrorMessage = "The field {0} must be greater than {1}." };

        ValidationContext validationContext
            = new(new ModelWithAnotherField<string> { AnotherField = fieldVal, AnotherField2 = fieldVal }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than AnotherField2."));
    }
}
