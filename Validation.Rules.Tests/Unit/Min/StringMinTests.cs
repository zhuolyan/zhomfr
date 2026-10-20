using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit.Min;

[TestFixture]
public class StringMinTests
{
    [TestCase("tes")]
    [TestCase("test")]
    [TestCase("test1")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        MinAttribute      attribute         = new("3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("test")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        MinAttribute      attribute         = new("5") { ErrorMessage  = "The field {0} length must be greater or equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp length must be greater or equal 5."));
    }
}
