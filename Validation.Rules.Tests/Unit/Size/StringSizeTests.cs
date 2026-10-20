using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit.Size;

[TestFixture]
public class StringSizeTests
{
    [TestCase("tes")]
    [TestCase("Tes")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        SizeAttribute     attribute         = new("3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("te")]
    [TestCase("Te")]
    [TestCase("test")]
    [TestCase("Test")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        SizeAttribute     attribute         = new("3") { ErrorMessage  = "The field {0} length must be equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp length must be equal 3."));
    }
}
