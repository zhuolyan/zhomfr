using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.Between;

[TestFixture(typeof(float))]
[TestFixture(typeof(double))]
[TestFixture(typeof(decimal))]
public class FloatBetweenTests<T> where T : IParsable<T>
{
    [TestCase("3.3")]
    [TestCase("4.5")]
    [TestCase("5.9")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        BetweenAttribute  attribute         = new("3.3", "5.9");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("3.299999")]
    [TestCase("5.900001")]
    public void Should_ReturnValidationError_When_IsInvalid(string inout)
    {
        BetweenAttribute  attribute         = new("3.3", "5.9") { ErrorMessage = "The field {0} must be between {1} and {2}." };
        ValidationContext validationContext = new(new()) { DisplayName         = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(inout, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be between 3.3 and 5.9."));
    }
}
