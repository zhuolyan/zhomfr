using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit.Max;

[TestFixture]
public class StringMaxTests
{
    [TestCase("tes")]
    [TestCase("test")]
    [TestCase("test1")]
    [TestCase("Test1")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        MaxAttribute      attribute         = new("5");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("test12")]
    [TestCase("Test12")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        MaxAttribute      attribute         = new("5") { ErrorMessage  = "The field {0} length must be less or equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp length must be less or equal 5."));
    }
}
