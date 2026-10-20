using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.Max;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class DateTimeMaxTests<T> where T : IParsable<T>
{
    [TestCase("2026-10-18 07:00:00")]
    [TestCase("2026-10-18 07:00:30")]
    [TestCase("2026-10-18 07:01:00")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        MaxAttribute      attribute         = new("2026-10-18 07:01:00");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2026-10-18 07:01:01")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        MaxAttribute      attribute         = new("2026-10-18 07:01:00") { ErrorMessage = "The field {0} must be less or equal {1}." };
        ValidationContext validationContext = new(new()) { DisplayName                  = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be less or equal 2026-10-18 07:01:00."));
    }
}
