using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.NotIn;

public class DecimalNotInTests
{
    [TestCase("0.000002")]
    [TestCase("1.3")]
    public void Should_ReturnSuccess_When_IsCorrect(string input)
    {
        NotInAttribute    attribute         = new("0.00001", "1.2");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("0.00001")]
    [TestCase("-0.00001")]
    [TestCase("1.2")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        NotInAttribute    attribute         = new("0.00001", "1.2", "-0.00001") { ErrorMessage = "The field {0} must be not equal any of: {1}." };
        ValidationContext validationContext = new(new()) { DisplayName                         = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(decimal.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be not equal any of: \"0.00001\", \"1.2\", \"-0.00001\"."));
    }
}
