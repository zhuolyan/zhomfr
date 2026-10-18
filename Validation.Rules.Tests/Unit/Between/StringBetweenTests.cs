using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit.Between;

[TestFixture]
public class StringBetweenTests
{
    [TestCase("tes")]
    [TestCase("test")]
    [TestCase("test1")]
    public void Should_ReturnSuccessResult_When_IsCorrectString(string value)
    {
        BetweenAttribute  attribute         = new("3", "5");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("tes")]
    [TestCase("test12")]
    public void Should_ReturnValidationError_When_IsIncorrectString(string value)
    {
        BetweenAttribute  attribute         = new("4", "5") { ErrorMessage = "The field {0} length must be between {1} and {2}." };
        ValidationContext validationContext = new(new()) { DisplayName     = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp length must be between 4 and 5."));
    }
}
