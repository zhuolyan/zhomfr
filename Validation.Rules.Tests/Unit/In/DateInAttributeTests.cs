using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Zhomfr.Validation.Rules.Tests.Unit.In;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class DateInAttributeTests<T> where T : IParsable<T>
{
    [TestCase("2026-10-18 07:00:00")]
    [TestCase("2026-10-19 07:00:00")]
    [TestCase("2026-10-20")]
    public void Should_ReturnSuccess_When_IsCorrect(string input)
    {
        InAttribute       attribute         = new("2026-10-18 07:00:00", "2026-10-19 07:00:00", "2026-10-20");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2026-10-18 07:00:00")]
    [TestCase("2026-10-19 07:00:00")]
    [TestCase("2026-10-20")]
    public void Should_ReturnValidationError_When_IsIncorrect(string input)
    {
        InAttribute       attribute         = new("2026-10-18", "2026-10-19", "2026-10-20 07:00:00") { ErrorMessage = "The field {0} must be equal one of: {1}." };
        ValidationContext validationContext = new(new()) { DisplayName                                              = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be equal one of: \"2026-10-18\", \"2026-10-19\", \"2026-10-20 07:00:00\"."));
    }
}
