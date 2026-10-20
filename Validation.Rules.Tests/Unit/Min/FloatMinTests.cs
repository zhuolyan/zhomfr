using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.Min;

[TestFixture(typeof(float))]
[TestFixture(typeof(double))]
[TestFixture(typeof(decimal))]
public class FloatMinTests<T> where T : IParsable<T>
{
    [TestCase("3.3")]
    [TestCase("4.5")]
    [TestCase("5.9")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        MinAttribute      attribute         = new("3.3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("3.299999")]
    public void Should_ReturnValidationError_When_IsInvalid(string inout)
    {
        MinAttribute      attribute         = new("3.3") { ErrorMessage = "The field {0} must be greater or equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName  = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(inout, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater or equal 3.3."));
    }
}
