using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.Size;

[TestFixture(typeof(float))]
[TestFixture(typeof(double))]
[TestFixture(typeof(decimal))]
public class FloatSizeTests<T> where T : IParsable<T>
{
    [TestCase("3.3")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        SizeAttribute     attribute         = new("3.3");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("3.300001")]
    [TestCase("3.299999")]
    public void Should_ReturnValidationError_When_IsInvalid(string inout)
    {
        SizeAttribute     attribute         = new("3.3") { ErrorMessage = "The field {0} must be equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName  = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(inout, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be equal 3.3."));
    }
}
